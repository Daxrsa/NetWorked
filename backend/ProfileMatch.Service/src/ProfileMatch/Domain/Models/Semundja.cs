﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Semundja
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int SpecializimiId { get; set; }
        public Specializimi Specializimi { get; set; }
    }
}
