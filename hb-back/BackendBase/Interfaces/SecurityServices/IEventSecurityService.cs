using BackendBase.Models;

namespace BackendBase.Interfaces.SecurityServices;

public interface IEventSecurityService {
    public Task validateCanUse(Event item);
    public Task validateCanCreate(Event item);
}
