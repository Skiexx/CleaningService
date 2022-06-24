#nullable disable

using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
using CleaningService.Models;
using CleaningService.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CleaningService.ViewModels;

public class InformationViewModel : ViewModelBase
{
    public InformationViewModel()
    {
        CleanPricings = new(Connector.GetContext().CleanPricings);
        ExitCommand = ReactiveCommand.Create<Window>(ExitImpl);
    }

    [Reactive] public ObservableCollection<CleanPricing> CleanPricings { get; set; }
    [Reactive] public CleanPricing SelectedCleanPricing { get; set; }

    public ReactiveCommand<Window, Unit> ExitCommand { get; set; }

    private void ExitImpl(Window window)
    {
        new UserWindow().Show();
        window.Close();
    }
}