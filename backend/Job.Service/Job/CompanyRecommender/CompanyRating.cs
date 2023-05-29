using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyRecommender
{
    //class qe permban fields e duhura nga dataset qe na nevojiten me i perdor
    public class CompanyRating
    {
        [LoadColumn(0)]
        public string userId;

        [LoadColumn(1)]
        public string companyId;

        [LoadColumn(2)]
        public bool Label;
    }
}
