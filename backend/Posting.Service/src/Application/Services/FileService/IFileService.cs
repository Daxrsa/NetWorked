using Microsoft.AspNetCore.Http;

namespace Application.Services.FileService
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile file);
        public bool DeleteImage(string fileName);
    }
}
