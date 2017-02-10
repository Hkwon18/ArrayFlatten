using System.Windows;
using ArrayFlatten.ViewModels;

namespace ArrayFlatten.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TreeViewModel();
        }
    }
}
