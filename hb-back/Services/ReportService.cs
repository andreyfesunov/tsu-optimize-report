using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BackendBase.Services
{
    public class ReportService : IReportService
    {
        private readonly FileRepository _fileRepository;
        private readonly ActivityRepository _activityRepository;
        private readonly LessonTypeRepository _lessonTypeRepository;
        private readonly RecordRepository _recordRepository;
        private readonly StateUserRepository _stateUserRepository;

        public ReportService(
            FileRepository fileRepository,
            ActivityRepository activityRepository,
            LessonTypeRepository lessonTypeRepository,
            RecordRepository recordRepository,
            StateUserRepository stateUserRepository
            )
        {
            _fileRepository = fileRepository;
            _activityRepository = activityRepository;
            _lessonTypeRepository = lessonTypeRepository;
            _recordRepository = recordRepository;
            _stateUserRepository = stateUserRepository;
        }

        public async Task<int> CreateReport(Guid stateUserId, IFormFile file)
        {
            // TODO add validation of stateUser

            if (!file.FileName.EndsWith(".xls"))
            {
                throw new Exception("Please, put '.xls' document");
            }

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var package = new HSSFWorkbook(stream);
            var worksheetCount = package.NumberOfSheets;

            if (worksheetCount <= 1)
            {
                throw new Exception("Workbook is incorrect, too few worksheets");
            }

            var activities = await _activityRepository.GetAll();
            var stateUser = await _stateUserRepository.GetById(stateUserId);

            for (var worksheetNumber = 1; worksheetNumber < worksheetCount; worksheetNumber++)
            {
                await _handleWorksheet(package.GetSheetAt(worksheetNumber), stateUser, activities);
            }

            // TODO add file saving

            return 1;
        }

        private async Task _handleWorksheet(ISheet worksheet, StateUser stateUser, List<Activity> activities)
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

                    if (contentCell is not { CellType: CellType.Numeric }) continue;

                    var hours = (int)contentCell.NumericCellValue;

                    await _recordRepository.AddEntity(new Record
                    {
                        LessonType = lessonType,
                        Activity = activity,
                        Hours = hours,
                        Id = Guid.NewGuid(),
                        StateUser = stateUser
                    });
                }

                row += 1;
                cell = worksheet.GetRow(row)?.GetCell(1); // Move to the next row and get the lesson name cell
            }
        }

        private async Task<LessonType> _resolveLessonType(string lessonName)
        {
            /*
             * TODO get rid of db calling every time. add memoization/get all lessonTypes 
             */
            var lessonType = await _lessonTypeRepository.GetLessonTypeByName(lessonName);

            return lessonType ?? await _lessonTypeRepository.AddEntity(new LessonType { Name = lessonName, Id = Guid.NewGuid() });
        }
    }
}
