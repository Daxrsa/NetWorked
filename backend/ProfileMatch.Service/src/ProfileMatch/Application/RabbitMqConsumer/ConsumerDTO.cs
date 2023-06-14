using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RabbitMqConsumer
{
    public class ConsumerDTO
    {
        public string JobRequirements { get; set; }
        public string ApplicantSkills { get; set; }
        public int ApplicationId { get; set; }
    }
}
