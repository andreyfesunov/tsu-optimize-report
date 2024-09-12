using Microsoft.AspNetCore.Http;

namespace Tsu.IndividualPlan.Domain.Interfaces.Utils;

public interface IStorage
{
    public Task<string> SaveFileAsync(IFormFile file, string? path = null);
}