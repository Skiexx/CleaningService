#nullable disable

using System;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
using CleaningService.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace CleaningService.ViewModels;

public class ChangePasswordViewModel : ViewModelBase
{
    public ChangePasswordViewModel()
    {
        SavePassword = ReactiveCommand.Create<Window>(SavePasswordImpl);
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
    }

    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatNewPassword { get; set; }

    public ReactiveCommand<Window, Unit> SavePassword { get; }
    public ReactiveCommand<Window, Unit> Exit { get; }

    private void SavePasswordImpl(Window window)
    {
        if (MainWindowViewModel.AuthorizedUser.Password != OldPassword)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Неверный старый пароль ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .ShowDialog(window);
            return;
        }

        if (NewPassword != RepeatNewPassword)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Пароли не совпадают ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .ShowDialog(window);
            return;
        }

        MainWindowViewModel.AuthorizedUser.Password = NewPassword;
        try
        {
            Connector.GetContext().Users.Update(MainWindowViewModel.AuthorizedUser);
            Connector.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandardWindow("Успешно",
                    "Пароль успешно изменён, войдите заново.",
                    ButtonEnum.Ok,
                    Icon.Success)
                .ShowDialog(window)
                .GetAwaiter()
                .OnCompleted(() =>
                {
                    new MainWindow().Show();
                    window.Close();
                });
        }
        catch (Exception)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Пароль не был сохранён ",
                    ButtonEnum.Ok,
                    Icon.Error)
                .ShowDialog(window)
                .GetAwaiter()
                .OnCompleted(Connector.ReloadContext);
        }
    }

    private void ExitImpl(Window window)
    {
        new ProfileWindow().Show();
        window.Close();
    }
}