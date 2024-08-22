using BackendBase.Dto.Report;

namespace BackendBase.Dto;

public class RecordDto
{
    public RecordDto(
        Guid Id,
        int Hours,
        ActivityDto? Activity,
        ReportListDto? StateUser,
        LessonTypeDto? LessonType
    )
    {
        this.Id = Id;
        this.Hours = Hours;
        this.Activity = Activity;
        this.StateUser = StateUser;
        this.LessonType = LessonType;
    }

    public readonly Guid Id;
    public readonly int Hours;
    public readonly ActivityDto? Activity;
    public readonly ReportListDto? StateUser;
    public readonly LessonTypeDto? LessonType;
}
