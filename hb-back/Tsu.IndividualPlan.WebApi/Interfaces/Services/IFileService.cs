using File = Tsu.IndividualPlan.WebApi.Models.File;

namespace Tsu.IndividualPlan.WebApi.Interfaces.Services;

public interface IFileService
{
    public Task<File> AddEntity(File entity);
}