using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Interfaces.Utils;
using Tsu.IndividualPlan.Domain.Models.Business;
using Tsu.IndividualPlan.Domain.Models.Project;
using Tsu.IndividualPlan.Transfer.Interfaces.Report;
using File = Tsu.IndividualPlan.Domain.Models.Business.File;
using NPOI.XSSF.UserModel;
using Tsu.IndividualPlan.Data.Context;
using ClosedXML.Excel;
using Tsu.IndividualPlan.Transfer.Extensions.Lib;
using Activity = Tsu.IndividualPlan.Domain.Models.Business.Activity;

namespace Tsu.IndividualPlan.Transfer.Services.Report;

public class ReportCreateService(
    IActivityRepository activityRepository,
    ILessonTypeRepository lessonTypeRepository,
    IRecordRepository recordRepository,
    IStateUserRepository stateUserRepository,
    IFileService fileService,
    IStorage storage,
    UserInfo userInfo,
    DataContext context)
    : IReportCreateService
{
    public async Task<bool> CreateReport(Guid stateUserId, IFormFile file)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                // TODO add validation of StateUserId

                using var package = _openWorkbook(file);
                var worksheetCount = package.NumberOfSheets;

                if (worksheetCount <= 1)
                    throw new Exception("Workbook is incorrect, too few worksheets");

                var activities = await activityRepository.GetAll();
                var stateUser = await stateUserRepository.GetById(stateUserId);

                for (var worksheetNumber = 1; worksheetNumber < worksheetCount; worksheetNumber++)
                    await _handleWorksheet(package.GetSheetAt(worksheetNumber), stateUser, activities);

                await _saveFile(file, stateUser.Id);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    private static void _deleteLastWorksheet(string path)
    {
        using (var workbook = new XLWorkbook(path))
        {
            var lastSheet = workbook.Worksheets.Last();
            lastSheet.Delete();
            workbook.Save();
        }
    }

    private IWorkbook _openWorkbook(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Файл не был загружен или пуст");

        string extension = Path.GetExtension(file.FileName).ToLower();

        using (Stream stream = file.OpenReadStream())
        {
            return extension switch
            {
                ".xls" => new HSSFWorkbook(stream), //Excel 97-2003
                ".xlsx" => new XSSFWorkbook(stream), //Excel 2007+
                _ => throw new ArgumentException(
                    $"Неподдерживаемый формат файла: {extension}. " +
                    "Поддерживаются только .xls и .xlsx")
            };
        }
    }

    private async Task<File> _saveFile(IFormFile file, Guid stateUserId)
    {
        var fileName =
            userInfo.GetUserId()
            + "/"
            + Guid.NewGuid()
            + Path.GetExtension(file.FileName);

        await storage.SaveFileAsync(file, fileName);

        if (Path.GetExtension(file.FileName) == ".xls")
        {
            var newFileName = fileName.Replace("xls", "xlsx");
            storage.ConvertXlsToXlsx(fileName, newFileName);
            fileName = newFileName;
        }

        return await fileService.AddEntity(
            new File(Path: fileName, StateUserId: stateUserId, CreatedDate: DateTime.UtcNow)
        );
    }

    private async Task _handleWorksheet(
        ISheet worksheet,
        StateUser stateUser,
        ICollection<Activity> activities
    )
    {
        Dictionary<string, ICell> cellFoundCache = new Dictionary<string, ICell>();

        var disciplineHeaderCell = worksheet.FindCell("Наименование дисциплины, практики и её тип, испытания государственной итоговой аттесатции", cellFoundCache);
        var row = disciplineHeaderCell.Address.Row + 1;
        const string endString = "Всего за семестр";
        var groupStringCellNum = worksheet.FindCell("Индекс учебной группы", cellFoundCache).Address.Column;
        var semestrId = worksheet.SheetName.Contains("Осень") ? 1 : 2;

        var cell = worksheet.GetRow(row).GetCell(disciplineHeaderCell.Address.Column);

        while (cell != null && cell.StringCellValue != endString)
        {
            if (cell.StringCellValue != string.Empty)
            {
                var lessonName = cell.StringCellValue;
                var lessonType = await _resolveLessonType(lessonName);

                foreach (var activity in activities)
                {
                    var activityHeaderCell = worksheet.FindCell(_takeFirstWords(activity.Name, 4), cellFoundCache); //_takeFirstWords потому что "Лабораторные работы, клинические практические " в первой части обрезаны
                    var contentCell = worksheet.GetRow(row).GetCell(activityHeaderCell.Address.Column);

                    var hours = contentCell.CellType switch
                    {
                        CellType.Numeric => (int)contentCell.NumericCellValue,
                        _ => int.Parse(contentCell.StringCellValue)
                    };

                    if (hours <= 0)
                        continue;

                    await recordRepository.AddEntity(
                        new Record(
                            LessonTypeId: lessonType.Id,
                            ActivityId: activity.Id,
                            Hours: hours,
                            StateUserId: stateUser.Id,
                            GroupString: worksheet.GetRow(row).GetCell(groupStringCellNum).StringCellValue,
                            SemestrId: semestrId
                        )
                    );
                }
            }

            row += 1;
            cell = worksheet.GetRow(row)?.GetCell(1);
        }
    }

    private async Task<LessonType> _resolveLessonType(string lessonName)
    {
        /*
         * TODO get rid of db calling every time. add memoization/get all lessonTypes
         */
        var lessonType = await lessonTypeRepository.GetLessonTypeByName(lessonName);

        return lessonType
               ?? await lessonTypeRepository.AddEntity(new LessonType(lessonName));
    }

    private string _takeFirstWords(string str, int wordsCount)
        => string.Join(" ", str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Take(wordsCount));
}