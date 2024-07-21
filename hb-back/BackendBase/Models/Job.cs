namespace BackendBase.Models
{
    public class Job : Base
    {
        public ICollection<State> States { get; set; }
        public string Name { get; set; }
    }
}
