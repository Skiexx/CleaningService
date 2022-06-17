using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using CleaningService.Core;
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

        public static string? Login { get; set; }

        public string? Password { get; set; }

        public ReactiveCommand<Window, Unit> EnterCommand { get; set; }
        public ReactiveCommand<Window, Unit> RegistrationCommand { get; set; }

        private void EnterCommandImpl(Window window)
        {
            var user = Connector.GetContext().Users.FirstOrDefault(u => u.Login.Equals(Login) && u.Password.Equals(Password));
            if (user == null)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Пользователь не найден! ", ButtonEnum.Ok,
                    Icon.Error).Show();
                return;
            }
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