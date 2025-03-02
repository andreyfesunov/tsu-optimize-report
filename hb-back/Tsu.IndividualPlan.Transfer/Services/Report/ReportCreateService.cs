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

namespace Tsu.IndividualPlan.Transfer.Services.Report;

public class ReportCreateService(
    IActivityRepository activityRepository,
    ILessonTypeRepository lessonTypeRepository,
    IRecordRepository recordRepository,
    IStateUserRepository stateUserRepository,
    IFileService fileService,
    IStorage storage,
    UserInfo userInfo)
    : IReportCreateService
{
    public async Task<bool> CreateReport(Guid stateUserId, IFormFile file)
    {
        // TODO add validation of StateUserId
        if (!file.FileName.EndsWith(".xls"))
            throw new Exception("Please, put '.xls' document");

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        using var package = new HSSFWorkbook(stream);
        var worksheetCount = package.NumberOfSheets;

        if (worksheetCount <= 1)
            throw new Exception("Workbook is incorrect, too few worksheets");

        var activities = await activityRepository.GetAll();
        var stateUser = await stateUserRepository.GetById(stateUserId);

        for (var worksheetNumber = 1; worksheetNumber < worksheetCount; worksheetNumber++)
            await _handleWorksheet(package.GetSheetAt(worksheetNumber), stateUser, activities);

        await _saveFile(file, stateUser.Id);

        return true;
    }

    private async Task<File> _saveFile(IFormFile file, Guid stateUserId)
    {
        var fileName =
            userInfo.GetUserId()
            + "/"
            + Guid.NewGuid()
            + Path.GetExtension(file.FileName);
        var path = await storage.SaveFileAsync(file, fileName);

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
        /*
         * TODO get rid of HARDCODE
         *
         * NPOI - считает с нуля
         *
         * 5 - строка, с которой начинаются отчёты.
         * "Всего за семестр" - строка, которой оканчивается часть, необходимая для считывания.
         */
        var row = 5;
        const string endString = "Всего за семестр";

        var cell = worksheet.GetRow(row).GetCell(1);

        while (cell != null && cell.StringCellValue != endString)
        {
            var lessonName = cell.StringCellValue;
            var lessonType = await _resolveLessonType(lessonName);

            foreach (var activity in activities)
            {
                var contentCell = worksheet.GetRow(row).GetCell(activity.Column);

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
                        StateUserId: stateUser.Id
                    )
                );
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
}