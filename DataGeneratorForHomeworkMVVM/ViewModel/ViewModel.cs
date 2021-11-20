
using System.ComponentModel;

using System.Windows.Input;

namespace DataGeneratorForHomework.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        DelegateCommand StartGeneration;

        public event PropertyChangedEventHandler PropertyChanged;

        public DataGeneratorForHomework.Model.DataGenerator DataGenerator { get; } = new Model.DataGenerator();

        public ViewModel()
        {
            StartGeneration = new DelegateCommand(DataGenerator.GenerateAsync, (o) => DataGenerator.Count>0 && DataGenerator.TemplateFilename!="");
            DataGenerator.ProgressChanged += DataGenerator_ProgressChanged;
        }

        private void DataGenerator_ProgressChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Progress"));
        }

        public ICommand Start
            {
            get => StartGeneration;                        
            }

        public int Count
        {
            get => DataGenerator.Count; 
            set
            {
                DataGenerator.Count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        }

        public string TemplateFilename
        {
            get => DataGenerator.TemplateFilename; 
            set
            {
                DataGenerator.TemplateFilename = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TemplateFilename"));
            }
        }

        public string Progress
        {
            get => DataGenerator.Progress;
        }

    }
}
