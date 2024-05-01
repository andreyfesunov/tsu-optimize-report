using BackendBase.Data;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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

            var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var package = new ExcelPackage(stream);
            var worksheetCount = package.Workbook.Worksheets.Count;

            if (worksheetCount <= 1)
            {
                throw new Exception("Workbook is incorrect, too few worksheets");
            }

            var activities = await _activityRepository.GetAll();
            var stateUser = await _stateUserRepository.GetById(stateUserId);

            for (var worksheetNumber = 1; worksheetNumber < worksheetCount; worksheetNumber++)
            {
                _handleWorksheet(package.Workbook.Worksheets[worksheetNumber], stateUser, activities);
            }

            // TODO add file saving

            return 1;
        }

        private async void _handleWorksheet(ExcelWorksheet worksheet, StateUser stateUser, List<Activity> activities)
        {
            /*
             * TODO get rid of HARDCODE
             *
             * 6 - строка, с которой начинаются отчёты.
             * "Всего за семестр" - строка, которой оканчивается часть, необходимая для считывания.
             */
            var row = 6;
            const string endString = "Всего за семестр";

            var lessonName = worksheet.Cells[row, 2].Value.ToString();
            while (lessonName != endString)
            {
                var lessonType = await _resolveLessonType(lessonName ?? throw new InvalidOperationException());

                foreach (var activity in activities)
                {
                    var content = worksheet.Cells[row, activity.Column].Value.ToString();

                    await _recordRepository.AddEntity(
                        new Record
                        {
                            LessonType = lessonType,
                            Activity = activity,
                            Hours = int.Parse(content ?? throw new InvalidOperationException()),
                            Id = Guid.NewGuid(),
                            StateUser = stateUser
                        });
                }

                row += 1;
                lessonName = worksheet.Cells[row, 2].Value.ToString();
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
