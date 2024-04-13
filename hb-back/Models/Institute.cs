using System.ComponentModel.DataAnnotations;

namespace BackendBase.Models
{
    public class Institute : Base
    {
        public ICollection<Department> Departments { get; set; }

        public string Name { get; set; }
    }
}
