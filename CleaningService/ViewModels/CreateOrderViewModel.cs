#nullable disable

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
using CleaningService.Models;
using CleaningService.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CleaningService.ViewModels;

public class CreateOrderViewModel : ViewModelBase
{
    public CreateOrderViewModel()
    {
        ExitCommand = ReactiveCommand.Create<Window>(ExitImpl);
        SaveOrder = ReactiveCommand.Create<Window>(SaveImpl);
        CleaningType = new(Connector.GetContext().CleanPricings);
        NewOrder = new()
        {
            IdCleanPriceNavigation = Connector.GetContext()
                .CleanPricings.First(pricing => pricing.Title == "Стандартная уборка")
        };
    }

    [Reactive] public Order NewOrder { get; set; }
    [Reactive] public ObservableCollection<CleanPricing> CleaningType { get; set; }
    [Reactive] public string RoomSize { get; set; }

    public ReactiveCommand<Window, Unit> ExitCommand { get; set; }
    public ReactiveCommand<Window, Unit> SaveOrder { get; set; }

    private void SaveImpl(Window window)
    {
        try
        {
            PayList pay = new PayList()
            {
                Sum = decimal.Parse(RoomSize) * NewOrder.IdCleanPriceNavigation.PriceForM2
            };
            Connector.GetContext().PayLists.Add(pay);
            Connector.GetContext().SaveChanges();
            NewOrder.IdPayListNavigation = Connector.GetContext().PayLists.OrderBy(x => x.Id).Last();
            NewOrder.IdUserNavigation = MainWindowViewModel.AuthorizedUser;
            Connector.GetContext().Orders.Add(NewOrder);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Заказ успешно добавлен ",
                    ButtonEnum.Ok,
                    Icon.Success)
                .ShowDialog(window)
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    new UserWindow().Show();
                    window.Close();
                });
        }
        catch (Exception)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Заказ не добавлен",
                    ButtonEnum.Ok,
                    Icon.Error)
                .ShowDialog(window);
            Connector.ReloadContext();
        }
    }

    private void ExitImpl(Window window)
    {
        new UserWindow().Show();
        window.Close();
    }
}