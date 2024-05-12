using BackendBase.Dto.Report;
using BackendBase.Models;

namespace BackendBase.Dto
{
    public class RecordDto
    {
        public string Id { get; set; }
        public ActivityDto Activity { get; set; }
        public ReportListDto StateUser { get; set; }
        public LessonTypeDto LessonType { get; set; }
        public int Hours { get; set; }
    }
}
