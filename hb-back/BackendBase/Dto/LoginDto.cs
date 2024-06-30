using System.ComponentModel.DataAnnotations;

namespace BackendBase.Dto
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
