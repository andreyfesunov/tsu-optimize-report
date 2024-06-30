namespace BackendBase.Dto.Report;

public class ReportDetailDto : ReportListDto
{
    public ICollection<EventDto> Events { get; set; }
}