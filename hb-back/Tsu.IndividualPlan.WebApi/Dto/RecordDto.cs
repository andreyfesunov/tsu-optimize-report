using Tsu.IndividualPlan.WebApi.Dto.Report;

namespace Tsu.IndividualPlan.WebApi.Dto;

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

    public Guid Id { get; init; }
    public int Hours { get; init; }
    public ActivityDto? Activity { get; init; }
    public ReportListDto? StateUser { get; init; }
    public LessonTypeDto? LessonType { get; init; }
}