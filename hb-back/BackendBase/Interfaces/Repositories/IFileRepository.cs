using File = BackendBase.Models.File;

namespace BackendBase.Interfaces.Repositories
{
    public interface IFileRepository
    {
        public Task<File> AddEntity(File entity);
    }
}
