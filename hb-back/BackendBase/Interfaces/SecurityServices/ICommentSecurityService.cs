using BackendBase.Models;

namespace BackendBase.Interfaces.SecurityServices;

public interface ICommentSecurityService {
    public Task validateCanUse(Comment item);
    public Task validateCanCreate(Comment item);
}
