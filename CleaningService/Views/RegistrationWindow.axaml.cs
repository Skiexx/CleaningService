using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CleaningService.ViewModels;

namespace CleaningService.Views;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        DataContext = new RegistrationWindowViewModel();
        ShowInTaskbar = true;
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}