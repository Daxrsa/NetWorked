using Microsoft.ML.Data;

namespace CompanyRecommender
{
    internal class Program
    {
        private static string BaseModelRelativePath = @"../../../Model";
        private static string ModelRelativePath = $"{BaseModelRelativePath}/model.zip";

        private static string BaseDataSetRelativepath = @"../../../Data";
        private static string TrainingDataRelativePath = $"{BaseDataSetRelativepath}/ratings_train.csv";
        private static string TestDataRelativePath = $"{BaseDataSetRelativepath}/ratings_test.csv";

        private static string TrainingDataLocation = GetAbsolutePath(TrainingDataRelativePath);
        private static string TestDataLocation = GetAbsolutePath(TestDataRelativePath);
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public static void DataPrep()
        {

            string[] dataset = File.ReadAllLines($"{BaseDataSetRelativepath}/company_ratings.csv");

            string[] new_dataset = new string[dataset.Length];
            new_dataset[0] = dataset[0];
            for (int i = 1; i < dataset.Length; i++)
            {
                string line = dataset[i];
                string[] lineSplit = line.Split(',');
                double rating = Double.Parse(lineSplit[2]);
                rating = rating > 3 ? 1 : 0;
                lineSplit[2] = rating.ToString();
                string new_line = string.Join(',', lineSplit);
                new_dataset[i] = new_line;
            }
            dataset = new_dataset;
            int numLines = dataset.Length;
            var body = dataset.Skip(1);
            var sorted = body.Select(line => new { SortKey = Int32.Parse(line.Split(',')[3]), Line = line })
                             .OrderBy(x => x.SortKey)
                             .Select(x => x.Line);
            File.WriteAllLines(@"../../../Data\ratings_train.csv", dataset.Take(1).Concat(sorted.Take((int)(numLines * 0.9))));
            File.WriteAllLines(@"../../../Data\ratings_test.csv", dataset.Take(1).Concat(sorted.TakeLast((int)(numLines * 0.1))));
        }

        public static float Sigmoid(float x)
        {
            return (float)(100 / (1 + Math.Exp(-x)));
        }

        public static string GetAbsolutePath(string relativeDatasetPath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativeDatasetPath);

            return fullPath;
        }
    }
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
    //kjo klase perdoret per me i inicializu predictions
    public class CompanyRatingPrediction
    {
        public bool PredictedLabel;

        public float Score;
    }
}