namespace BackendBase.Models
{
    public class State
    {
        public Guid Id { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public int Count { get; set; }
        public int Hours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }  
}
