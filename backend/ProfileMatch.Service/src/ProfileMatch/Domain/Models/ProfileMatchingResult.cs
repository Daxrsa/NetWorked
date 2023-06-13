using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProfileMatchingResult
    {
        [Key]
        public Guid Id { get; set; }
        public double Result { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
