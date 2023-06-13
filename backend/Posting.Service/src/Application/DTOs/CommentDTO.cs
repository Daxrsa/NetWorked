namespace Application.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public string Fullname { get; set; }
        //edhe nji image property na duhet ma von
    }
}