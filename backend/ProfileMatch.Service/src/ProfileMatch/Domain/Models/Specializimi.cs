﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Specializimi
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Semundja> Semundjet { get; set; }
    }
}
