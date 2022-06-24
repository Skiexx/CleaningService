using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CleaningService.ViewModels;

namespace CleaningService.Views;

public partial class CreateOrderWindow : Window
{
    public CreateOrderWindow()
    {
        DataContext = new CreateOrderViewModel();
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