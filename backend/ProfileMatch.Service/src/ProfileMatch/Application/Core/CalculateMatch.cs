using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Core
{
    public class CalculateMatch
    {
        //me i mar parasysh edhe reviews te kompanive, to be done later
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


        //Kjo metod kthen numrin e skills te ngjashme ne mes te kerkesave te job dhe aftesive te aplikuesit
        public int CountSimilarities(List<string> jobRequirements, List<string> applicantSkills)
        {
            HashSet<string> uniqueSkills = new HashSet<string>(jobRequirements.SelectMany(skill => skill.ToLower().Split(' ')));
            int count = 0;

            foreach (string skill in applicantSkills)
            {
                string[] skillWords = skill.ToLower().Split(' ');

                foreach (string word in skillWords)
                {
                    if (uniqueSkills.Contains(word))
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        //return matching percentage
        public double GetPercentage(int similarities, int requirements)
        {
            try
            {
                double result = ((double)similarities / requirements) * 100;
                return Math.Round((Double)result, 2);
            }catch (DivideByZeroException ex)
            {
                throw new InvalidOperationException("Divisor cannot be zero.", ex);
            }
        }

        //just a test method
        public double GetScore()
        {
            List<string> jobReq = new List<string> { "Frontend Developer","Java","Javascript","Backend", "Full stack", "DevKm"};
            List<string> skills = new List<string> { "Frontend", "java","full", "StaCk", "DevkM"};

            int similarities = CountSimilarities(jobReq, skills);
            int length = jobReq.Count();

            return GetPercentage(similarities, length);
        }
    }
}
