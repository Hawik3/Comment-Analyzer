using Comment_Analyzer.Model.Excel;
using Comment_Analyzer.Model.SentimentAnalysis;
using Comment_Analyzer.Services;
<<<<<<< Updated upstream
=======
using Comment_Analyzer.View;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
>>>>>>> Stashed changes
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Comment_Analyzer.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
<<<<<<< Updated upstream
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
=======
        private RelayCommand? _openCommand;
        SentimentModel? _sentimentModel = null;
        private string _filePath = "";
        private int _selectedTabIndex;
        private bool _isSentimentAnalysisWasOpen = false;
        private bool _isTimeLineWasOpen = false;
        private bool _isFileSelected = false;
        private IEnumerable<ExcelSentimentTable> _sentimentScores = [];
        private float _averageSentimentScore;
        private int _commentTextColumn = 7;
        private int _dateTimeColumn = 2;
        private ISeries[] _commentTimeline = [];
>>>>>>> Stashed changes
        public List<int> Numbers { get; } = Enumerable.Range(1, 10).ToList();
        public int CommentTextColumn
        {
<<<<<<< Updated upstream
            get { return _column; }
=======
            get => _commentTextColumn;
>>>>>>> Stashed changes
            set
            {
                _commentTextColumn = value;
                OnPropertyChanged();
            }
        }
        public int DateTimeColumn
        {
            get => _dateTimeColumn;
            set
            {
                _dateTimeColumn = value;
                OnPropertyChanged();
            }
        }
        public float AverageSentimentScore
        {
            get { return _averageSentimentScore; }
            set
            {
                _averageSentimentScore = value;
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
<<<<<<< Updated upstream
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
=======
        public bool IsFileSelected
        {
            get { return _isFileSelected; }
            set
            {
                _isFileSelected = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<ExcelSentimentTable> SentimentScores
        {
            get { return _sentimentScores; }
            set
            {
                _sentimentScores = value;
                OnPropertyChanged();
            }
        }
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
        public ISeries[] CommentTimeline
        {
            get { return _commentTimeline; }
            set
            {
                _commentTimeline = value;
                OnPropertyChanged();
            }
        }
        public List<Axis> XAxis { get; } = [ new Axis{Labeler = (value) => (value + "h").ToString() } ];
        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand ??=
                    new RelayCommand(obj =>
                    {
                        _isFileSelected = false;
                        OpenFileDialog openFileDialog = new()
                        {
                            Filter = "Excel table (*.xlsx)|*.xlsx"
                        };
                        if (openFileDialog.ShowDialog() == true)
                        {
                            FilePath = openFileDialog.FileName;
                        }
                        _isSentimentAnalysisWasOpen = false;
                        _isTimeLineWasOpen = false;

                    });
            }
        }

        public async void TabSelected(int index)
        {
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    if (!_isSentimentAnalysisWasOpen)
                    {
                        _sentimentModel = new();
                        var progressWindow = new ProgressWindow("Predict Sentiment");
                        IEnumerable<ExcelSentimentTable> table = _sentimentModel.PredictFile(FilePath, CommentTextColumn);

                        await Task.Run(() =>
                        {
                            AverageSentimentScore = table.Average(x => x.Score);
                            SentimentScores = table;

                            _isSentimentAnalysisWasOpen = true;
                        });

                        progressWindow.Close();
                        
                    }
                    break;
                case 2:
                    if (!_isTimeLineWasOpen)
                    {
                        CommentTimeline = CommentsTimeline.CommentsToTimeline(FilePath, DateTimeColumn) ?? [];
                        _isTimeLineWasOpen = true;
                    }
                    break;
            }
        }

>>>>>>> Stashed changes



        public MainViewModel()
        {
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
