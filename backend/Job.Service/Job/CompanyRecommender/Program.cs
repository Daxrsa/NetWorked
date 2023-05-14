using Microsoft.ML;
using Microsoft.ML.Data;
using System.Drawing;

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
            //Call the following piece of code for splitting the ratings.csv into ratings_train.csv and ratings.test.csv.
           // Program.DataPrep();

            //STEP 1: Create MLContext to be shared across the model creation workflow objects
            MLContext mlContext = new MLContext();

            //STEP 2: Read data from text file using TextLoader by defining the schema for reading the movie recommendation datasets and return dataview.
            var trainingDataView = mlContext.Data.LoadFromTextFile<CompanyRating>(path: TrainingDataLocation, hasHeader: true, separatorChar: ',');

            Console.WriteLine("=============== Reading Input Files ===============");
            Console.WriteLine();

            // ML.NET doesn't cache data set by default. Therefore, if one reads a data set from a file and accesses it many times, it can be slow due to
            // expensive featurization and disk operations. When the considered data can fit into memory, a solution is to cache the data in memory. Caching is especially
            // helpful when working with iterative algorithms which needs many data passes. Since SDCA is the case, we cache. Inserting a
            // cache step in a pipeline is also possible, please see the construction of pipeline below.
            trainingDataView = mlContext.Data.Cache(trainingDataView);

            Console.WriteLine("=============== Transform Data And Preview ===============");
            Console.WriteLine();

            //STEP 4: Transform your data by encoding the two features userId and movieID.
            //        These encoded features will be provided as input to FieldAwareFactorizationMachine learner
            var dataProcessPipeline = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "userIdFeaturized", inputColumnName: nameof(CompanyRating.userId))
                                          .Append(mlContext.Transforms.Text.FeaturizeText(outputColumnName: "companyIdFeaturized", inputColumnName: nameof(CompanyRating.companyId))
                                          .Append(mlContext.Transforms.Concatenate("Features", "userIdFeaturized", "companyIdFeaturized")));

            // STEP 5: Train the model fitting to the DataSet
            Console.WriteLine("=============== Training the model ===============");
            Console.WriteLine();
            var trainingPipeLine = dataProcessPipeline.Append(mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new string[] { "Features" }));
            var model = trainingPipeLine.Fit(trainingDataView);

            //STEP 6: Evaluate the model performance
            Console.WriteLine("=============== Evaluating the model ===============");
            Console.WriteLine();
            var testDataView = mlContext.Data.LoadFromTextFile<CompanyRating>(path: TestDataLocation, hasHeader: true, separatorChar: ',');

            var prediction = model.Transform(testDataView);


            //STEP 7:  Try/test a single prediction by predicting a single movie rating for a specific user
            Console.WriteLine("=============== Test a single prediction ===============");
            Console.WriteLine();
            var predictionEngine = mlContext.Model.CreatePredictionEngine<CompanyRating, CompanyRatingPrediction>(model);
            CompanyRating testData = new CompanyRating() { userId = "3", companyId = "2" };

            var companyRatingPrediction = predictionEngine.Predict(testData);
            Console.WriteLine($"UserId:{testData.userId} with companyd: {testData.companyId} Score:{Sigmoid(companyRatingPrediction.Score)} and Label {companyRatingPrediction.PredictedLabel}", Color.YellowGreen);
            Console.WriteLine();

            //STEP 8:  Save model to disk
            Console.WriteLine("=============== Writing model to the disk ===============");
            Console.WriteLine(); mlContext.Model.Save(model, trainingDataView.Schema, ModelPath);

            Console.WriteLine("=============== Re-Loading model from the disk ===============");
            Console.WriteLine();
            ITransformer trainedModel;
            using (FileStream stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                trainedModel = mlContext.Model.Load(stream, out var modelInputSchema);
            }

            Console.WriteLine("Press any key ...");
            Console.Read();

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