using Microsoft.AspNetCore.Http;

namespace File.Package.FileService
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile file);
        public bool DeleteImage(string fileName);
    }
}
