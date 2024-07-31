using BackendBase.Models;

namespace BackendBase.Interfaces.SecurityServices;

public interface ILessonSecurityService
{
    public Task validateCanUse(Lesson item);
    public Task validateCanCreate(Lesson item);
}
