using File = Tsu.IndividualPlan.WebApi.Models.File;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Repositories;

public interface IFileRepository
{
    public Task<File> AddEntity(File entity);
}