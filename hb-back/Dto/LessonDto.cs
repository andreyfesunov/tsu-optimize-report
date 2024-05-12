namespace BackendBase.Dto
{
    public class LessonDto
    {
        public string Id { get; set; }
        public EventDto Event { get; set; }
        public LessonTypeDto LessonType { get; set; }
        public DateTime FactDate { get; set; }
        public DateTime PlanDate { get; set; }
    }
}
