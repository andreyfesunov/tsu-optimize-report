using AutoMapper;
using BackendBase.Dto;
using BackendBase.Dto.Report;
using BackendBase.Interfaces;
using BackendBase.Models;
using BackendBase.Repositories;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace BackendBase.Services
{
    public class ReportService : IReportService
    {
        private readonly FileRepository _fileRepository;
        private readonly ActivityRepository _activityRepository;
        private readonly LessonTypeRepository _lessonTypeRepository;
        private readonly RecordRepository _recordRepository;
        private readonly StateUserRepository _stateUserRepository;
        private readonly IMapper _mapper;

        public ReportService(
            FileRepository fileRepository,
            ActivityRepository activityRepository,
            LessonTypeRepository lessonTypeRepository,
            RecordRepository recordRepository,
            StateUserRepository stateUserRepository,
            IMapper mapper
            )
        {
            _fileRepository = fileRepository;
            _activityRepository = activityRepository;
            _lessonTypeRepository = lessonTypeRepository;
            _recordRepository = recordRepository;
            _stateUserRepository = stateUserRepository;
            _mapper = mapper;
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

            var activitiesDto = await _activityRepository.GetAll();
            var activities = activitiesDto.Select(x => _mapper.Map<Activity>(x)).ToList();
            var stateUserDto = await _stateUserRepository.GetById(stateUserId);
            var stateUser = _mapper.Map<StateUser>(stateUserDto);

            for (var worksheetNumber = 1; worksheetNumber < worksheetCount; worksheetNumber++)
            {
                await _handleWorksheet(package.GetSheetAt(worksheetNumber), stateUser, activities);
            }

            // TODO add file saving

            return 1;
        }

        private async Task _handleWorksheet(ISheet worksheet, StateUser stateUser, ICollection<Activity> activities)
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

                    if (contentCell is not { CellType: CellType.String }) continue;

                    var hours = int.Parse(contentCell.StringCellValue);

                    if (hours <= 0) continue;

                    await _recordRepository.AddEntity(new Record
                    {
                        LessonTypeId = lessonType.Id,
                        ActivityId = activity.Id,
                        Hours = hours,
                        Id = Guid.NewGuid(),
                        StateUserId = stateUser.Id
                    });
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
            var lessonType = await _lessonTypeRepository.GetLessonTypeByName(lessonName);

            return lessonType ?? await _lessonTypeRepository.AddEntity(new LessonType { Name = lessonName, Id = Guid.NewGuid() });
        }
    }
}
