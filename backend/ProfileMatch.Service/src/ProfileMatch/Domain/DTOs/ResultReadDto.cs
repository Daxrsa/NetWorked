namespace Domain.DTOs
{
    public class ResultReadDto
    {
        public Guid Id { get; set; }
        public double Result { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
