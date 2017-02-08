using ArrayFlatten.ViewModel;
using System.Windows;

namespace ArrayFlatten
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
