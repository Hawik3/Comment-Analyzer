using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Comment_Analyzer.View
{
    /// <summary>
    /// Логика взаимодействия для ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {

        public ProgressWindow(string Text, bool addProgressBar, int MaxValue = 0 )
        {
            InitializeComponent();
            progressBar.IsEnabled = addProgressBar;
            progressBar.Maximum = MaxValue;
            textBlock.Text = Text;
            

        }
       
    }
}
