using File = Tsu.IndividualPlan.Domain.Models.Business.File;

namespace Tsu.IndividualPlan.Domain.Interfaces.Repositories;

public interface IFileRepository
{
    Task<File> AddEntity(File entity);
}