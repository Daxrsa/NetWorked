using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TestDTOs
{
    public class AddKomentDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int ArticleId { get; set; }
    }
}
