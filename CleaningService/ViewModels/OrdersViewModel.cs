#nullable disable

using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
using CleaningService.Models;
using CleaningService.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CleaningService.ViewModels;

public class OrdersViewModel : ViewModelBase
{
    public OrdersViewModel()
    {
        OrdersDataGrid =
            new ObservableCollection<Order>(Connector.GetContext()
                .Orders.Where(o => o.IdUser == MainWindowViewModel.AuthorizedUser.Id));
        ExitCommand = ReactiveCommand.Create<Window>(ExitImpl);
    }

    [Reactive] public ObservableCollection<Order> OrdersDataGrid { get; set; }
    [Reactive] public Order SelectedOrder { get; set; }
    public ReactiveCommand<Window, Unit> ExitCommand { get; set; }

    private void ExitImpl(Window window)
    {
        new UserWindow().Show();
        window.Close();
    }
}