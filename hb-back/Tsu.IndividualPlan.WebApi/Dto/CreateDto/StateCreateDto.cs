namespace Tsu.IndividualPlan.WebApi.Dto.CreateDto;

public class StateCreateDto
{
    public StateCreateDto(
        Guid JobId,
        DateTime StartDate,
        DateTime EndDate,
        int Count = 1,
        int Hours = 1485
    )
    {
        this.JobId = JobId;
        this.Hours = Hours;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.Count = Count;
    }

    public int Count { get; init; }
    public int Hours { get; init; }
    public Guid JobId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}