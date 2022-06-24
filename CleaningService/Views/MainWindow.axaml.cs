using Avalonia.Controls;
using CleaningService.ViewModels;

namespace CleaningService.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            ShowInTaskbar = true;
            InitializeComponent();
        }
    }
}