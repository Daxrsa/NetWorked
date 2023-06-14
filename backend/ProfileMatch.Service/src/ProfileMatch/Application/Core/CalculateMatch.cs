namespace Application.Core
{
    public class CalculateMatch
    {
        //me i mar parasysh edhe manual vleresimin nga rekruteri
        public int GetReview(string review)
        {
            string upperReview = review.ToUpper();

            switch (upperReview)
            {
                case "BAD":
                    return 0;
                case "GOOD":
                    return 3;
                case "PERFECT":
                    return 5;
                default:
                    return 0;
            }
        }

        //Kjo metod ka mu perdor later ne permiresim
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

        //simplified method
        public int CountSimilarities(string jobRequirements, string skills)
        {
            jobRequirements = jobRequirements.ToLower();
            skills = skills.ToLower();

            string[] jobReqs = jobRequirements.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] applicantSkills = skills.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            var similarities = jobReqs.Intersect(applicantSkills).ToList();
            return similarities.Count;
        }

        //return matching percentage
        public double GetPercentage(int similarities, string jobRequirements, int? review)
        {
            try
            {
                string[] jobReqs = jobRequirements.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                int length = jobReqs.Length;
                double result = ((double)similarities / length) * 100;
                if(review.HasValue)
                {
                    result += review.Value;
                }
                return Math.Round((Double)result, 2);
            }catch (DivideByZeroException ex)
            {
                throw new InvalidOperationException("Divisor cannot be zero.", ex);
            }
        }

        //just a test method
       /* public double GetScore()
        {
            List<string> jobReq = new List<string> { "Frontend Developer","Java","Javascript","Backend", "Full stack", "DevKm"};
            List<string> skills = new List<string> { "Frontend", "java","full", "StaCk", "DevkM"};

            int similarities = CountSimilarities(jobReq, skills);
            int length = jobReq.Count();

            return GetPercentage(similarities, length,3);
        }*/
    }
}
