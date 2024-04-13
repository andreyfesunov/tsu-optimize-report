using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class Department : Base
    {
        public ICollection<State> States { get; set; }
        public Institute Insitute { get; set; }
        public string Name { get; set; }
    }
}
