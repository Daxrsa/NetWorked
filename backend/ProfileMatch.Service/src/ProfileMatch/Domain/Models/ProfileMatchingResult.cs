using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProfileMatchingResult
    {
        [Key]
        public Guid Id { get; set; }
        public double Result { get; set; }
    }
}
