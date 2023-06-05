using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReadDTOs
{
    public class ResultReadDto
    {
        public Guid Id { get; set; }
        public double Result { get; set; }
        public string ApplicationId { get; set; }
    }
}
