using System.Reactive;
using Avalonia.Controls;
using CleaningService.Views;
using ReactiveUI;

namespace CleaningService.ViewModels;

public class UserWindowViewModel : ViewModelBase
{
    public UserWindowViewModel()
    {
        ProfileCommand = ReactiveCommand.Create<Window>(ProfileImpl);
        InformationCommand = ReactiveCommand.Create<Window>(InformationImpl);
        OrdersCommand = ReactiveCommand.Create<Window>(OrdersImpl);
        CreateOrderCommand = ReactiveCommand.Create<Window>(CreateOrderImpl);
        ExitCommand = ReactiveCommand.Create<Window>(ExitImpl);
    }

    public ReactiveCommand<Window, Unit> ProfileCommand { get; set; }
    public ReactiveCommand<Window, Unit> InformationCommand { get; set; }
    public ReactiveCommand<Window, Unit> OrdersCommand { get; set; }
    public ReactiveCommand<Window, Unit> CreateOrderCommand { get; set; }
    public ReactiveCommand<Window, Unit> ExitCommand { get; set; }

    private void InformationImpl(Window window)
    {
        new InformationWindow().Show();
        window.Close();
    }

    private void ProfileImpl(Window window)
    {
        new ProfileWindow().Show();
        window.Close();
    }

    private void OrdersImpl(Window window)
    {
        new OrdersWindow().Show();
        window.Close();
    }

    private void CreateOrderImpl(Window window)
    {
        new CreateOrderWindow().Show();
        window.Close();
    }

    private void ExitImpl(Window window)
    {
        new MainWindow().Show();
        window.Close();
    }
}