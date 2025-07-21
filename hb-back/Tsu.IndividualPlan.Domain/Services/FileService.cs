using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
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

    private const string _libreOfficeWindowsPath = @"C:\Program Files\LibreOffice\program\soffice.exe";
    public void ConvertXlsToXlsx(string inputFile, string outputDir)
    {
        inputFile = Path.Combine(_root, inputFile);
        outputDir = Path.Combine(_root, outputDir);
        var command = "libreoffice";
        var arguments = $"--headless --convert-to xlsx \"{inputFile}\" ";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (!System.IO.File.Exists(_libreOfficeWindowsPath))
            {
                throw new FileNotFoundException("LibreOffice не найден по указанному пути", command);
            }

            command = _libreOfficeWindowsPath;
        }

        using var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();

        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (process.ExitCode != 0 || !string.IsNullOrWhiteSpace(error))
        {
            throw new Exception($"Error converting the file to xlsx via LibreOffice. Error: {error}");
        }

        System.IO.File.Delete(inputFile);
    }
}