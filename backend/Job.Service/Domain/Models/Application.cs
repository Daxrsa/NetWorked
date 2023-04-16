using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateApplied { get; set; }

        //Job
        public Guid JobId { get; set; }
        public Job Job { get; set; }

        //Applicant
    }
}
