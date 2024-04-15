using Comment_Analyzer.Model;
using Comment_Analyzer.Model.Excel;
using Comment_Analyzer.Model.SentimentAnalysis;
using Comment_Analyzer.Services;
using Comment_Analyzer.View;
using LiveChartsCore;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Comment_Analyzer.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isFileSelected = false;
        public bool IsFileSelected
        {
            get { return _isFileSelected; }
            set
            {
                _isFileSelected = value;
                OnPropertyChanged();
            }
        }
        private IEnumerable<ExcelSentimentTable> _scores = [];
        public IEnumerable<ExcelSentimentTable> Scores
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
            get => _column;
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
                IsFileSelected = true;
            }
        }
        private string _filePath = "";
        private int _selectedTabIndex;
        private bool _isSentimentAnalysisWasOpen = false;
        private bool _isTimeLineWasOpen = false;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;

                    OnPropertyChanged();
                    TabSelected(value);
                }
            }
        }
        private ISeries[] _series = [];

        public ISeries[] Series
        {
            get { return _series; }
            set
            {
                _series = value;
                OnPropertyChanged();
            }
        }

        public void TabSelected(int index)
        {
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    if (!_isSentimentAnalysisWasOpen)
                    {
                        Scores = _sentimentModel.PredictFile(FilePath, Column);
                        var ProgressWindow = new ProgressWindow("Adding data to list", false);
                        Score = Scores.Average(x => x.Score);
                        _isSentimentAnalysisWasOpen = true;
                    }
                    break;
                case 2:
                    if (!_isTimeLineWasOpen)
                    {
                        Series = CommentsTimeline.CommentsToTimeline(FilePath, 2) ?? [];
                        _isSentimentAnalysisWasOpen = true;
                    }
                    break;


            }
        }

        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand ??=
                    new RelayCommand(obj =>
                       {
                           OpenFileDialog openFileDialog = new()
                           {
                               Filter = "Excel table (*.xlsx)|*.xlsx"
                           };
                           if (openFileDialog.ShowDialog() == true)
                           {
                               FilePath = openFileDialog.FileName;
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
