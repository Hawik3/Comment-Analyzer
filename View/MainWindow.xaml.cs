using System.Windows;
using System.Windows.Controls;



namespace Comment_Analyzer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> styles = new List<string> { "View/Resources/DarkTheme.xaml", "View/Resources/LightTheme.xaml" };
        bool _isDarkTheme = false;
        public MainWindow()
        {
            InitializeComponent();



        }
        private void ThemeChange(object sender, RoutedEventArgs e)
        {
            if (_isDarkTheme)
            {
                var uri = new Uri(styles[1], UriKind.Relative);
                // загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.Clear();
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                _isDarkTheme = false;
            }
            else
            {
                var uri = new Uri(styles[0], UriKind.Relative);
                // загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                // очищаем коллекцию ресурсов приложения
                Application.Current.Resources.Clear();
                // добавляем загруженный словарь ресурсов
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                _isDarkTheme = true;
            }
        }
    }
}