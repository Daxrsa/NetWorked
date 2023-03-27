using Common.Library;

namespace Posting.Service.Models
{
    public class Post : IEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
    }
}