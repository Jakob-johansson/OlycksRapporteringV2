using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using MauiApp = Microsoft.Maui.Controls.Application;
namespace OlycksRapporteringV2.MAUI.ViewModels

{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly UserRepositoryDb _repo;

        public event PropertyChangedEventHandler PropertyChanged;

        private string loginPortal;

        public LoginViewModel(string portal)
        {
            _repo = new UserRepositoryDb();
            LoginCommand = new Command(async () => await Login());
            loginPortal = portal;
        }

        private string employeeId;
        public string EmployeeId
        {
            get => employeeId;
            set
            {
                employeeId = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        private async Task Login()
        {
            var user = await _repo.GetUserByEmployeeIdOrEmail(EmployeeId);

            if (user == null)
            {
                await MauiApp.Current.MainPage.DisplayAlert("Error", "User not found", "OK");
                return;
            }

            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                await MauiApp.Current.MainPage.DisplayAlert("Error", "Wrong password", "OK");
                return;
            }

            // Kontrollera portal
            if (loginPortal == "EmployeePortal" && user.Role != UserRole.Employee)
            {
                await MauiApp.Current.MainPage.DisplayAlert(
                    "Åtkomst nekad",
                    "Logga in via ledningsportalen.",
                    "OK");
                return;
            }

            if (loginPortal == "SafetyPortal" && user.Role == UserRole.Employee)
            {
                await MauiApp.Current.MainPage.DisplayAlert(
                    "Åtkomst nekad",
                    "Anställda måste använda personalportalen.",
                    "OK");
                return;
            }

            // Starta session
            UserSession.Instance.StartSession(user);

            await MauiApp.Current.MainPage.DisplayAlert(
                "Success",
                $"Welcome {user.EmployeeId}!",
                "OK");

            // Navigera beroende på roll
            if (user.Role == UserRole.Employee)
            {
                MauiApp.Current.MainPage = new NavigationPage(new LoggedInHomePage());
            }
            else
            {
                MauiApp.Current.MainPage = new NavigationPage(new OlycksRapporteringV2.MAUI.Views.LoggedInAdminPage());
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}