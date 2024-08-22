using AutoMapper;
using BackendBase.Dto;
using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Interfaces.Services.Report;
using BackendBase.Interfaces.Utils;
using BackendBase.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using File = BackendBase.Models.File;

namespace BackendBase.Services.Report;

public class ReportCreateService : IReportCreateService
{
    private readonly IActivityRepository _activityRepository;
    private readonly ILessonTypeRepository _lessonTypeRepository;
    private readonly IMapper _mapper;
    private readonly IRecordRepository _recordRepository;
    private readonly IStateUserRepository _stateUserRepository;
    private readonly IFileService _fileService;
    private readonly IStorage _storage;
    private readonly UserInfo _userInfo;

    public ReportCreateService(
        IActivityRepository activityRepository,
        ILessonTypeRepository lessonTypeRepository,
        IRecordRepository recordRepository,
        IStateUserRepository stateUserRepository,
        IFileService fileService,
        IStorage storage,
        UserInfo userInfo,
        IMapper mapper
    )
    {
        _activityRepository = activityRepository;
        _lessonTypeRepository = lessonTypeRepository;
        _recordRepository = recordRepository;
        _stateUserRepository = stateUserRepository;
        _fileService = fileService;
        _storage = storage;
        _userInfo = userInfo;
        _mapper = mapper;
    }

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

        var activitiesDto = await _activityRepository.GetAll();
        var activities = activitiesDto.Select(x => _mapper.Map<Activity>(x)).ToList();
        var stateUser = await _stateUserRepository.GetById(stateUserId);

        for (var worksheetNumber = 1; worksheetNumber < worksheetCount; worksheetNumber++)
            await _handleWorksheet(package.GetSheetAt(worksheetNumber), stateUser, activities);

        await _saveFile(file, stateUser.Id);

        return true;
    }

    private async Task<File> _saveFile(IFormFile file, Guid stateUserId)
    {
        var fileName =
            _userInfo.GetUserId()
            + "/"
            + Guid.NewGuid().ToString()
            + Path.GetExtension(file.FileName);
        var path = await _storage.SaveFileAsync(file, fileName);

        return await _fileService.AddEntity(
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

                if (contentCell is not { CellType: CellType.String })
                    continue;

                var hours = int.Parse(contentCell.StringCellValue);

                if (hours <= 0)
                    continue;

                await _recordRepository.AddEntity(
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
        var lessonType = await _lessonTypeRepository.GetLessonTypeByName(lessonName);

        return lessonType
            ?? await _lessonTypeRepository.AddEntity(new LessonType(Name: lessonName));
    }
}
