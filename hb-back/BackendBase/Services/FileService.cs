using BackendBase.Interfaces.Repositories;
using BackendBase.Interfaces.Services;
using BackendBase.Interfaces.Utils;
using File = BackendBase.Models.File;

namespace BackendBase.Services;

public class FileService : IFileService, IStorage
{
    private readonly string _root;
    private readonly IFileRepository _repository;

    public FileService(IWebHostEnvironment env, IConfiguration conf, IFileRepository repository)
    {
        _root = Path.Combine(env.ContentRootPath, conf["Storage:Folder"]);
        _repository = repository;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string? name = null)
    {
        var path = Path.Combine(_root, name ?? file.FileName);
        var dir = Path.GetDirectoryName(path);
        if (dir != null && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using var stream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);

        return path;
    }

    public Task<File> AddEntity(File entity) => _repository.AddEntity(entity);
}
