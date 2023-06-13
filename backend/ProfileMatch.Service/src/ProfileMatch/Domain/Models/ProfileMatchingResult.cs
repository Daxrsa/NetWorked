﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProfileMatchingResult
    {
        [Key]
        public Guid Id { get; set; }
        public double Result { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
