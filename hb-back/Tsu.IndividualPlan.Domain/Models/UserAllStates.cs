using Tsu.IndividualPlan.Domain.Models.Business;

namespace Tsu.IndividualPlan.Domain.Models
{
    public class UserAllStates
    {
        public User User { get; set; }
        public IEnumerable<State> States { get; set; }
    }
}
