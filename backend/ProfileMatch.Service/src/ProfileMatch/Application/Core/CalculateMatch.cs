using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class CalculateMatch
    {
        public int GetReview(string review)
        {
            if (review.ToUpper().Equals(Review.BAD))
            {
                return 0;
            }
            else if (review.ToUpper().Equals(Review.GOOD))
            {
                return 2;
            }
            else if (review.ToUpper().Equals(Review.VERY_GOOD))
            {
                return 3;
            }
            else if (review.ToUpper().Equals(Review.PERFECT))
            {
                return 5;
            }
            return 0;
        }

        public double GetScore(string skills)
        {
            return 0;
        }
    }
}
