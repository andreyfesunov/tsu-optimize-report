namespace BackendBase.Models
{
    public class Rank : Base
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
