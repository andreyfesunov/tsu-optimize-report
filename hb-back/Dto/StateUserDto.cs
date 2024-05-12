using BackendBase.Models;

namespace BackendBase.Dto
{
    public class StateUserDto
    {
        public string Id { get; set; }
        public StateDto State { get; set; }
        public UserDto User { get; set; }
        public double Rate { get; set; }
        public string Status { get; set; }
    }
}
