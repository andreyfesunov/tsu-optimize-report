namespace Tsu.IndividualPlan.WebApi.Interfaces.Utils;

public interface IStorage
{
    public Task<string> SaveFileAsync(IFormFile file, string? path = null);
}