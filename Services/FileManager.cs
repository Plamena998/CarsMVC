using Core;
using Microsoft.AspNetCore.Http;
namespace Services
{
    public class FileManager(IAppEnvironment environment)
    {
        private readonly IAppEnvironment _env = environment;

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var mainPath = _env.WebRootPath;
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(mainPath, "images", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return "/images/" + fileName;
        }
    }
}