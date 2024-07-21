using BackendBase.Models.Enum;

namespace BackendBase.Dto.Report;

public class ReportListDto
{
    public string Id { get; set; }
    public StateDto State { get; set; }
    public double Rate { get; set; }
    public StateUserStatus Status { get; set; }
}