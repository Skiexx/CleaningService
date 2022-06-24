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
    private User _user = new();

    public RegistrationWindowViewModel()
    {
        RegistrationCommand = ReactiveCommand.Create<Window>(RegistrationCommandImpl);
        ExitCommand = ReactiveCommand.Create<Window>(ExitCommandImpl);
    }

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
            User.IdRoleNavigation = Connector.GetContext().Roles.FirstOrDefault(role => role.Title == "Пользователь")!;
            Connector.GetContext().Users.Add(User);
            Connector.GetContext().SaveChanges();
            MainWindowViewModel.Login = User.Login;
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Ползователь успешно зарегестрирован")
                .ShowDialog(window).GetAwaiter().OnCompleted(() =>
                {
                    new MainWindow().Show();
                    window.Close();
                });
        }
        catch (Exception)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Такой логин уже существует! ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .ShowDialog(window)
                .GetAwaiter()
                .OnCompleted(Connector.ReloadContext);
        }
    }

    private void ExitCommandImpl(Window window)
    {
        new MainWindow().Show();
        window.Close();
    }
}