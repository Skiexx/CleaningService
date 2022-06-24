using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
using CleaningService.Models;
using CleaningService.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace CleaningService.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            EnterCommand = ReactiveCommand.Create<Window>(EnterCommandImpl);
            RegistrationCommand = ReactiveCommand.Create<Window>(RegistrationCommandImpl);
        }

        public static User AuthorizedUser { get; set; } = null!;

        public static string? Login { get; set; }

        public string? Password { get; set; }

        public ReactiveCommand<Window, Unit> EnterCommand { get; set; }
        public ReactiveCommand<Window, Unit> RegistrationCommand { get; set; }

        private void EnterCommandImpl(Window window)
        {
            var user = Connector.GetContext().Users.FirstOrDefault(u => u.Login.Equals(Login));
            if (user == null)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Пользователь не найден! ", ButtonEnum.Ok,
                    Icon.Error).ShowDialog(window);
                return;
            }

            if (!user.Password.Equals(Password))
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                        "Введеный пароль неверный, повторите попытку",
                        ButtonEnum.Ok,
                        Icon.Error)
                    .ShowDialog(window);
                return;
            }

            AuthorizedUser = user;
            new UserWindow().Show();
            window.Close();
        }

        private void RegistrationCommandImpl(Window window)
        {
            new RegistrationWindow().Show();
            window.Close();
        }
    }
}