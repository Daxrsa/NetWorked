namespace JobService.Services.Interfaces
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile file);
        public bool DeleteImage(string fileName);
    }
}
