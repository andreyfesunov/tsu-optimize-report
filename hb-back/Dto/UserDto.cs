using System.ComponentModel.DataAnnotations;
using BackendBase.Models.Enum;

namespace BackendBase.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public RoleUserEnum Role { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
