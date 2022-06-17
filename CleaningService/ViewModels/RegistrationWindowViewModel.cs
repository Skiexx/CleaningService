using System;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
using CleaningService.Models;
using CleaningService.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace CleaningService.ViewModels;

public class RegistrationWindowViewModel : ViewModelBase
{
    public RegistrationWindowViewModel()
    {
        RegistrationCommand = ReactiveCommand.Create<Window>(RegistrationCommandImpl);
        ExitCommand = ReactiveCommand.Create<Window>(ExitCommandImpl);
    }
    private User _user = new User();

    public User User
    {
        get => _user; 
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    public ReactiveCommand<Window, Unit> RegistrationCommand { get; set; }
    public ReactiveCommand<Window, Unit> ExitCommand { get; set; }

    private void RegistrationCommandImpl(Window window)
    {
        if (User.Login == null || 
            User.Password == null || 
            User.PhoneNumber == null || 
            User.FirstName == null ||
            User.LastName == null || 
            User.MiddleName == null)
        {
            MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка", "Не все поля заполнены! ", ButtonEnum.Ok, Icon.Error)
                .ShowDialog(window);
            return;
        }

        try
        {
            User.IdRole = Connector.GetContext().Roles.FirstOrDefault(role => role.Title == "Клиент")!.Id;
            Connector.GetContext().Users.Add(User);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Ползователь успешно зарегестрирован")
                .ShowDialog(window);
            MainWindowViewModel.Login = User.Login;
            new MainWindow().Show();
            window.Close();
        }
        catch (Exception ex)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Такой логин уже существует! ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .ShowDialog(window);
            Connector.ReloadContext();
        }
    }

    private void ExitCommandImpl(Window window)
    {
        new MainWindow().Show();
        window.Close();
    }
}