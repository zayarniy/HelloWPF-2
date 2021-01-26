using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGeneratorForHomework.Model
{
    class DataGenerator
    {
        public int Count { get; set; } = 1000;
        public string TemplateFilename { get; set; } = "data.txt";
        public Random Random { get; } = new Random();

        public void Generate()
        {
            for (int i = 0; i < Count; i++)
            {
                StreamWriter sw = new StreamWriter(Path.GetDirectoryName(TemplateFilename)+"\\"+ Path.GetFileNameWithoutExtension(TemplateFilename)+i+Path.GetExtension(TemplateFilename));
                sw.WriteLine("{0} {1} {2}", Random.Next(1, 3), Random.NextDouble(), Random.NextDouble());
                sw.Close();
            }
        }

    }
}
