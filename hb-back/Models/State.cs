namespace BackendBase.Models
{
    public class State : Base
    {
        public ICollection<StateUser> StatesUsers { get; set; }
        public Department Department { get; set; }
        public Job Job { get; set; }
        public int Count { get; set; }
        public int Hours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
