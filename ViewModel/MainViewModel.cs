using Comment_Analyzer.Model;
using Comment_Analyzer.Model.Excel;
using Comment_Analyzer.Model.SentimentAnalysis;
using Comment_Analyzer.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Win32;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows;

namespace Comment_Analyzer.ViewModel
{
    //public class SentimentAnalysisViewModel : INotifyPropertyChanged
    //{
    //    internal SentimentModel? _sentimentModel = null;
    //    internal bool _isSentimentAnalysisWasOpen = false;
    //    internal IEnumerable<ExcelSentimentTable> _sentimentScores = [];
    //    internal float _averageSentimentScore;
    //    public float AverageSentimentScore
    //    {
    //        get { return _averageSentimentScore; }
    //        set
    //        {
    //            _averageSentimentScore = value;
    //            OnPropertyChanged();
    //        }
    //    }



    //    public IEnumerable<ExcelSentimentTable> SentimentScores
    //    {
    //        get { return _sentimentScores; }
    //        set
    //        {
    //            _sentimentScores = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    internal void SentimentAnlaysisTabSelected(string FilePath, int CommentTextColumn)
    //    {
    //        if (!_isSentimentAnalysisWasOpen)
    //        {
    //            Thread analysis = new(StartAnalysis);
    //            analysis.Start();

    //        }
    //        void StartAnalysis()
    //        {
    //            _sentimentModel = new();
    //            IEnumerable<ExcelSentimentTable> table = _sentimentModel.PredictFile(FilePath, CommentTextColumn);
    //            AverageSentimentScore = table.Average(x => x.Score);
    //            SentimentScores = table;
    //            _isSentimentAnalysisWasOpen = true;
    //        }
    //    }
    //    public void OnPropertyChanged([CallerMemberName] string prop = "")
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //    }
    //    public event PropertyChangedEventHandler? PropertyChanged;
    //}
    //public class CommentTimelineViewModel : INotifyPropertyChanged 
    //{
    //    internal bool _isTimeLineWasOpen = false;
    //    internal ISeries[] _commentTimeline = [];
    //    public List<Axis> XAxis { get; } = [new Axis { Labeler = (value) => (value + "h").ToString() }];
    //    public ISeries[] CommentTimeline
    //    {
    //        get { return _commentTimeline; }
    //        set
    //        {
    //            _commentTimeline = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    internal void CommentTimelineTabSelected(string FilePath, int DateTimeColumn)
    //    {
    //        if (!_isTimeLineWasOpen)
    //        {
    //            ISeries[]? timeline = CommentsTimeline.CommentsToTimeline(FilePath, DateTimeColumn);
    //            if (timeline is not null)
    //            {
    //                CommentTimeline = timeline;

    //                _isTimeLineWasOpen = true;
    //            }
    //            else
    //            {

    //            }

    //        }

    //    }
    //    public void OnPropertyChanged([CallerMemberName] string prop = "")
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //    }
    //    public event PropertyChangedEventHandler? PropertyChanged;
    //}
    
    public class MainViewModel : INotifyPropertyChanged
    {
        
        public class CommentTimelineViewModel : INotifyPropertyChanged
        {
            internal bool _isNotCommentTimeLineLoaded = true;
            internal bool _isTimeLineWasOpen = false;
            internal ISeries[] _commentTimeline = [];
            public List<Axis> XAxis { get; } = [new Axis { Labeler = (value) => (value + "h").ToString() }];
            public ISeries[] CommentTimeline
            {
                get { return _commentTimeline; }
                set
                {
                    _commentTimeline = value;
                    OnPropertyChanged();
                }
            }
            public bool IsNotCommentTimeLineLoaded
            {
                get { return _isNotCommentTimeLineLoaded; }
                set
                {
                    _isNotCommentTimeLineLoaded = value;
                    OnPropertyChanged();
                }
            }
            internal void CommentTimelineTabSelected(string FilePath, int DateTimeColumn)
            {
                if (!_isTimeLineWasOpen)
                {
                    Thread timeline = new(Start);
                    timeline.Start();

                }
                void Start()
                {
                    ISeries[]? timeline = CommentsTimeline.CommentsToTimeline(FilePath, DateTimeColumn);
                    if (timeline is not null)
                    {
                        CommentTimeline = timeline;

                        _isTimeLineWasOpen = true;
                        IsNotCommentTimeLineLoaded = false;
                    }
                    else
                    {
                        //SwitchTabTo(0);
                    }
                }

            }
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
            public event PropertyChangedEventHandler? PropertyChanged;
        }
        public class SentimentAnalysisViewModel : INotifyPropertyChanged
        {

            internal bool _isNotSentimentAnalysisLoaded = true;
            internal SentimentModel? _sentimentModel = null;
            internal bool _isSentimentAnalysisWasOpen = false;
            internal IEnumerable<ExcelSentimentTable> _sentimentScores = [];
            internal float _averageSentimentScore;
            public bool IsNotSentimentAnalysisLoaded
            {
                get { return _isNotSentimentAnalysisLoaded; }
                set
                {
                    _isNotSentimentAnalysisLoaded = value;
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
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
            public event PropertyChangedEventHandler? PropertyChanged;


            public IEnumerable<ExcelSentimentTable> SentimentScores
            {
                get { return _sentimentScores; }
                set
                {
                    _sentimentScores = value;
                    OnPropertyChanged();
                }
            }
            internal void SentimentAnlaysisTabSelected(string FilePath, int CommentTextColumn)
            {
                if (!_isSentimentAnalysisWasOpen)
                {
                    Thread analysis = new(StartAnalysis);
                    analysis.Start();

                }
                void StartAnalysis()
                {
                    _sentimentModel = new();
                    IEnumerable<ExcelSentimentTable> table = _sentimentModel.PredictFile(FilePath, CommentTextColumn);
                    AverageSentimentScore = table.Average(x => x.Score);
                    SentimentScores = table;
                    _isSentimentAnalysisWasOpen = true;
                    IsNotSentimentAnalysisLoaded = false;
                }
            }
        }

        public SentimentAnalysisViewModel SentimentAnalysis { get; set; } = new SentimentAnalysisViewModel();
        public CommentTimelineViewModel CommentTimeline { get; set; } = new CommentTimelineViewModel();
        private RelayCommand? _openCommand;
        private string _filePath = "";
        private int _selectedTabIndex;
        private bool _isFileSelected = false;
        private int _commentTextColumn = 7;
        private int _dateTimeColumn = 2;
        public List<int> Numbers { get; } = Enumerable.Range(1, 10).ToList();
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
                        SentimentAnalysis._isSentimentAnalysisWasOpen = false;
                        CommentTimeline._isTimeLineWasOpen = false;

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
                    SentimentAnalysis.SentimentAnlaysisTabSelected(FilePath, CommentTextColumn);
                    break;
                case 2:
                    CommentTimeline.CommentTimelineTabSelected(FilePath, DateTimeColumn);
                    break;
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void SwitchTabTo(int index) => SelectedTabIndex = index;



    }
}
