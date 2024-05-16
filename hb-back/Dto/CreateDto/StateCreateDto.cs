namespace BackendBase.Dto.CreateDto
{
    public class StateCreateDto
    {
        public int Count { get; set; }
        public int Hours { get; set; }
        public string JobId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
