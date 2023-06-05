namespace Domain.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public string PathOrFilename { get; set; }
        public string FileType { get; set; }
    }
}