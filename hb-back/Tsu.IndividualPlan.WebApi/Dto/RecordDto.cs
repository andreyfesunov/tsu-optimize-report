using Tsu.IndividualPlan.WebApi.Dto.Report;

namespace Tsu.IndividualPlan.WebApi.Dto;

public class RecordDto(
    Guid Id,
    int Hours,
    string GroupString,
    ActivityDto? Activity,
    ReportListDto? StateUser,
    LessonTypeDto? LessonType)
{
    public Guid Id { get; init; } = Id;
    public int Hours { get; init; } = Hours;
    public string GroupString { get; init; } = GroupString;
    public ActivityDto? Activity { get; init; } = Activity;
    public ReportListDto? StateUser { get; init; } = StateUser;
    public LessonTypeDto? LessonType { get; init; } = LessonType;
}