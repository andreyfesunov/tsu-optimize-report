using BackendBase.Models;

namespace BackendBase.Dto
{
    public class EventTypeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkDto Work { get; set; }
    }
}
