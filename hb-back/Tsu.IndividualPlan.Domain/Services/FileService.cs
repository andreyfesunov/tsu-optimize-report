using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Tsu.IndividualPlan.Domain.Interfaces.Repositories;
using Tsu.IndividualPlan.Domain.Interfaces.Services;
using Tsu.IndividualPlan.Domain.Interfaces.Utils;
using File = Tsu.IndividualPlan.Domain.Models.Business.File;

namespace Tsu.IndividualPlan.Domain.Services;

public class FileService(IHostEnvironment env, IConfiguration conf, IFileRepository repository)
    : IFileService, IStorage
{
    private readonly string _root = Path.Combine(env.ContentRootPath,
        conf["Storage:Folder"] ?? throw new InvalidOperationException());

    public Task<File> AddEntity(File entity)
    {
        return repository.AddEntity(entity);
    }

    public async Task<string> SaveFileAsync(IFormFile file, string? name = null)
    {
        var path = Path.Combine(_root, name ?? file.FileName);
        var dir = Path.GetDirectoryName(path);
        if (dir != null && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

        await using var stream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);

        return path;
    }

    public string GetRoot()
        => _root;
}