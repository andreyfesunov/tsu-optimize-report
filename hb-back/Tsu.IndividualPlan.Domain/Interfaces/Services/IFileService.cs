using File = Tsu.IndividualPlan.Domain.Models.Business.File;

namespace Tsu.IndividualPlan.Domain.Interfaces.Services;

public interface IFileService
{
    public Task<File> AddEntity(File entity);
}