using Tsu.IndividualPlan.WebApi.Interfaces.Repositories;
using Tsu.IndividualPlan.WebApi.Interfaces.Services;
using Tsu.IndividualPlan.WebApi.Interfaces.Utils;
using File = Tsu.IndividualPlan.WebApi.Models.File;

namespace Tsu.IndividualPlan.WebApi.Services;

public class FileService : IFileService, IStorage
{
    private readonly IFileRepository _repository;
    private readonly string _root;

    public FileService(IWebHostEnvironment env, IConfiguration conf, IFileRepository repository)
    {
        _root = Path.Combine(env.ContentRootPath, conf["Storage:Folder"]);
        _repository = repository;
    }

    public Task<File> AddEntity(File entity)
    {
        return _repository.AddEntity(entity);
    }

    public async Task<string> SaveFileAsync(IFormFile file, string? name = null)
    {
        var path = Path.Combine(_root, name ?? file.FileName);
        var dir = Path.GetDirectoryName(path);
        if (dir != null && !Directory.Exists(dir)) Directory.CreateDirectory(dir);

        using var stream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);

        return path;
    }
}