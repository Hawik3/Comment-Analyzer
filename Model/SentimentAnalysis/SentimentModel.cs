using Microsoft.ML;
using Microsoft.ML.Data;
using System.Diagnostics;



namespace Comment_Analyzer.Model.SentimentAnalysis
{
    public class SentimentModel
    {
        string _pathToModel = @"..\..\..\Model\SentimentAnalysis\sentimentAnalysisModel.zip";
        string _pathToDataset = @"";
        PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;
        readonly ITransformer _model;
        readonly MLContext _MLcontext;
        public SentimentModel()
        {
            _MLcontext = new();
            _model = _MLcontext.Model.Load(_pathToModel, out var _);
            _predictionEngine = _MLcontext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        }
        public ITransformer Create()
        {
            var data = _MLcontext.Data.LoadFromTextFile<SentimentTrainData>(path: _pathToDataset, hasHeader: true);
            var trainData = _MLcontext.Data.TrainTestSplit(data, testFraction: 0.2);
            var textFeaturizer = _MLcontext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(SentimentTrainData.Text))
                .Append(_MLcontext.Regression.Trainers.LbfgsPoissonRegression(labelColumnName: "Label", featureColumnName: "Features"));
            ITransformer MLmodel = textFeaturizer.Fit(trainData.TrainSet);
            var modelPath = _pathToModel;
            _MLcontext.Model.Save(MLmodel, data.Schema, modelPath);
            return MLmodel;
        }
        public void EvaluateModel()
        {
            var data = _MLcontext.Data.LoadFromTextFile<SentimentTrainData>(path: _pathToDataset);
            var trainData = _MLcontext.Data.TrainTestSplit(data, testFraction: 0.2);
            var predictions = _model.Transform(trainData.TestSet);
            IDataView testDataPredictions = _model.Transform(predictions);
            var metrics = _MLcontext.Regression.Evaluate(testDataPredictions, "Label");
            Debug.WriteLine($"Accuracy: {metrics.MeanAbsoluteError}");
            Debug.WriteLine($"Accuracy: {metrics.MeanAbsoluteError}");

        }
        public void Predict(string comment)
        {
            var predictionFunction = _MLcontext.Model.CreatePredictionEngine<SentimentTrainData, SentimentTrainPrediction>(_model);
            SentimentTrainData sampleStatement = new()
            {
                Text = comment
            };

            var resultPrediction = predictionFunction.Predict(sampleStatement);
            Debug.WriteLine(resultPrediction.Score);
        }
        public IEnumerable<SentimentPrediction> PredictFile(List<string> comments)
        {
            
            foreach (var comment in comments)
            {
                SentimentData sampleStatement = new()
                {
                    Text = comment
                };
                var resultPrediction = _predictionEngine.Predict(sampleStatement);
                SentimentPrediction result = new()
                {
                    Score = resultPrediction.Score,
                    Text = comment
                };
                yield return result;
            }
            yield break;
        }
        public class SentimentTrainData
        {
            [LoadColumn(0)]
            public int Ignored1 { get; set; }

            [LoadColumn(1)]
            public string? Ignored2 { get; set; }

            [LoadColumn(2), ColumnName("Label")]
            public float Sentiment { get; set; }

            [LoadColumn(3)]
            public string? Text { get; set; }
        }
        public class SentimentTrainPrediction : SentimentTrainData
        {
            public float Score { get; set; }
        }
        public class SentimentData
        {
            [LoadColumn(0)]
            public string? Text { get; set; }
        }
        public class SentimentPrediction : SentimentData
        {
            public float Score { get; set; }
        }

    }


}

