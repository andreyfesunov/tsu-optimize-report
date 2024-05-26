using BackendBase.Dto.Report;
using BackendBase.Models;

namespace BackendBase.Dto
{
    public class EventDto
    {
        public string Id { get; set; }
        public EventTypeDto EventType { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
    }
}
