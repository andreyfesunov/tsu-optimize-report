using BackendBase.Models.Enum;

namespace BackendBase.Models
{
    public class User : Base
    {
        public ICollection<StateUser> StatesUsers { get; set; }
        public RoleUserEnum Role { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RankId { get; set; }
        public Rank Rank { get; set; }
        public Guid DegreeId { get; set; }
        public Degree Degree { get; set; }
    }
}
