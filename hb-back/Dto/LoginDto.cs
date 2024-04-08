using System.ComponentModel.DataAnnotations;

namespace BackendBase.Dto
{
    public class LoginDto
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
