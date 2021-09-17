///
/// https://www.youtube.com/watch?v=XBSvp43EQhA
/// 


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;

namespace LearnMachineByText
{
    internal class HumanData
    {
        [Microsoft.ML.Data.LoadColumn(1), Microsoft.ML.Data.ColumnName("Lastname")]
        public string Lastname { get; set; }

        [Microsoft.ML.Data.LoadColumn(2), Microsoft.ML.Data.ColumnName("Firstname")]
        public string Firstname { get; set; }

        [Microsoft.ML.Data.LoadColumn(3), Microsoft.ML.Data.ColumnName("Patronymic")]
        public string Patronymic { get; set; }

        [Microsoft.ML.Data.LoadColumn(4), Microsoft.ML.Data.ColumnName("Sex")]
        public string Sex { get; set; }

        [Microsoft.ML.Data.LoadColumn(5), Microsoft.ML.Data.ColumnName("DOB")]
        public string DOB { get; set; }

        [Microsoft.ML.Data.LoadColumn(6), Microsoft.ML.Data.ColumnName("Country")]
        public string Country { get; set; }

        [Microsoft.ML.Data.LoadColumn(7), Microsoft.ML.Data.ColumnName("City")]
        public string City { get; set; }

        [Microsoft.ML.Data.LoadColumn(8)]
        public string Phone { get; set; }

        [Microsoft.ML.Data.LoadColumn(9), Microsoft.ML.Data.ColumnName("Email")]
        public string Email { get; set; }
    }

    public class OutputDataML
    {
        public List<string> outputDataPath { private get; set; }

        OutputDataML(List<string> outputDataPath)
        {

        }

        public void Start(List<string> pathData)
        {
            try
            {
                foreach (var item in pathData)
                {
                    FileInfo fileInfo = new FileInfo(item);
                    if (fileInfo.Extension == ".csv")
                    {
                        MLContext mlContext = new MLContext();
                        var data = mlContext.Data.LoadFromTextFile<HumanData>(path: item, hasHeader: false, separatorChar: '<');

                        var split = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
                        var feature = split.TrainSet.Schema
                            .Select(col => col.Name)
                            .Where(colName => colName != "Country" && colName != "Phone")
                            .ToArray();

                        var pipline = mlContext.Transforms.Text.FeaturizeText("Text", "Phone")
                            .Append(mlContext.Transforms.Concatenate("Features", feature))
                            .Append(mlContext.Transforms.Concatenate("Features", "Features", "Text"))
                            .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression());

                        var model = pipline.Fit(split.TrainSet);

                        var predictions = model.Transform(split.TrainSet);

                        var metrics = mlContext.Regression.Evaluate(predictions);

                        Console.WriteLine($"R^2 - {metrics.RSquared}");
                    }

                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }


    }
}
