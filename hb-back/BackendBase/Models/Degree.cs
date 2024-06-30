namespace BackendBase.Models
{
    public class Degree : Base
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
