using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Notifications
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime NoticationCreated { get; set; }
    }
}
