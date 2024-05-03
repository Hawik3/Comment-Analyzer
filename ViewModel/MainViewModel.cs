using Comment_Analyzer.Model;
using Comment_Analyzer.Model.Excel;
using Comment_Analyzer.Model.SentimentAnalysis;
using Comment_Analyzer.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Comment_Analyzer.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
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
        public List<int> Numbers { get; } = Enumerable.Range(1, 10).ToList();
        public List<Axis> XAxis { get; } = [new Axis { Labeler = (value) => (value + "h").ToString() }];
        public int CommentTextColumn
        {
            get => _commentTextColumn;
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
                IsFileSelected = true;
            }
        }
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
        public void TabSelected(int index)
        {
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    SentimentAnlaysisTabSelected();
                    break;
                case 2:
                    CommentTimelineTabSelected();
                    break;
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void SwitchTabTo(int index) => SelectedTabIndex = index;
        private async void SentimentAnlaysisTabSelected()
        {
            if (!_isSentimentAnalysisWasOpen)
            {
                _sentimentModel = new();
                IEnumerable<ExcelSentimentTable> table = _sentimentModel.PredictFile(FilePath, CommentTextColumn);
                await Task.Run(() =>
                {
                    AverageSentimentScore = table.Average(x => x.Score);
                    SentimentScores = table;

                    _isSentimentAnalysisWasOpen = true;
                });
            }
        }
        private void CommentTimelineTabSelected()
        {
            if (!_isTimeLineWasOpen)
            {
                ISeries[]? timeline = CommentsTimeline.CommentsToTimeline(FilePath, DateTimeColumn);
                if (timeline != null)
                {
                    CommentTimeline = timeline;

                    _isTimeLineWasOpen = true;
                }
                else
                {
                    SwitchTabTo(0);
                }

            }

        }

    }
}
