using File = BackendBase.Models.File;

namespace BackendBase.Interfaces.Services;

public interface IFileService
{
    public Task<File> AddEntity(File entity);
}
