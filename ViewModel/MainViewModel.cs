using Comment_Analyzer.Model.Excel;
using Comment_Analyzer.Model.SentimentAnalysis;
using Comment_Analyzer.Services;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Comment_Analyzer.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IEnumerable<ExcelTable> _scores = [];
        public IEnumerable<ExcelTable> Scores
        {
            get { return _scores; }
            set
            {
                _scores = value;
                OnPropertyChanged();
            }
        }

        private float _score;
        private int _column = 7;
        public List<int> Numbers { get; } = Enumerable.Range(1, 10).ToList();
        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                OnPropertyChanged();
            }
        }
        public float Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }
        private string _filePath = "";

        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand ??= new RelayCommand(obj =>
                       {
                           OpenFileDialog openFileDialog = new()
                           {
                               Filter = "Excel table (*.xlsx)|*.xlsx"
                           };
                           if (openFileDialog.ShowDialog() == true)
                           {
                               FilePath = openFileDialog.FileName;
                               
                               Scores = _sentimentModel.PredictFile(openFileDialog.FileName, Column);

                               Score = Scores.Average(x => x.Score);
                           }

                       });
            }
        }

        private RelayCommand? _openCommand;
        readonly SentimentModel _sentimentModel;
        public MainViewModel()
        {
            _sentimentModel = new();
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
