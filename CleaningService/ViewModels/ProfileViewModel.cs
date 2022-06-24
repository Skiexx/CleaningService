using System;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
using CleaningService.Models;
using CleaningService.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace CleaningService.ViewModels;

public class ProfileViewModel : ViewModelBase
{
    private User _currentUser = MainWindowViewModel.AuthorizedUser!;

    public ProfileViewModel()
    {
        Exit = ReactiveCommand.Create<Window>(ExitImpl);
        SaveChanges = ReactiveCommand.Create<Window>(SaveChangesImpl);
        ChangePassword = ReactiveCommand.Create<Window>(ChangePasswordImpl);
    }

    public ReactiveCommand<Window, Unit> Exit { get; }
    public ReactiveCommand<Window, Unit> ChangePassword { get; }
    public ReactiveCommand<Window, Unit> SaveChanges { get; }

    public User CurrentUser
    {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }

    private void ExitImpl(Window window)
    {
        new UserWindow().Show();
        window.Close();
    }

    private void SaveChangesImpl(Window window)
    {
        try
        {
            Connector.GetContext().Users.Update(CurrentUser);
            Connector.GetContext().SaveChanges();
            MainWindowViewModel.Login = CurrentUser.Login;
            MessageBoxManager
                .GetMessageBoxStandardWindow("Успешно",
                    "Изменения успешно сохранены, войдите заново.",
                    ButtonEnum.Ok,
                    Icon.Success)
                .ShowDialog(window).GetAwaiter().OnCompleted(() =>
                {
                    new MainWindow().Show();
                    window.Close();
                });
        }
        catch (Exception)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Не удалось сохранить изменения",
                    ButtonEnum.Ok,
                    Icon.Error)
                .ShowDialog(window).GetAwaiter().OnCompleted(Connector.ReloadContext);
        }
    }

    private void ChangePasswordImpl(Window window)
    {
        new ChangePasswordWindow().Show();
        window.Close();
    }
}