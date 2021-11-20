using System;
using System.IO;
using System.Threading.Tasks;

namespace DataGeneratorForHomework.Model
{
    public class DataGenerator
    {
        private string _progress = "Not start";

        public int Count { get; set; } = 1000;

        public string TemplateFilename { get; set; } = "D:\\data.txt";

        public string Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                ProgressChanged?.Invoke();
            }
        }

        public Random Random { get; } = new Random();

        public event Action ProgressChanged;

        public async void GenerateAsync(object o)
        {
            await Task.Run(Generate);
            //Generate();
        }

        void Generate()
        {
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine("{0} {1} {2}", Random.Next(1, 3), Random.NextDouble(), Random.NextDouble());
                StreamWriter sw = new StreamWriter(Path.GetDirectoryName(TemplateFilename) + "\\" + Path.GetFileNameWithoutExtension(TemplateFilename) + i + Path.GetExtension(TemplateFilename));
                sw.WriteLine("{0} {1} {2}", Random.Next(1, 3), Random.NextDouble(), Random.NextDouble());
                sw.Close();
                Progress = i.ToString();
            }
            Progress = "Done";

        }

    }
}
