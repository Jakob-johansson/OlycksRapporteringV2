using BCrypt.Net;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using MauiApp = Microsoft.Maui.Controls.Application;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class CreateUserViewModel : INotifyPropertyChanged
    {
        private readonly UserRepositoryDb _repo;

        public event PropertyChangedEventHandler? PropertyChanged;

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

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }


        public List<InsuranceCompany> InsuranceCompanies { get; } = Enum.GetValues(typeof(InsuranceCompany)).Cast<InsuranceCompany>().ToList();
        private InsuranceCompany selectedInsuranceCompany;
        public InsuranceCompany SelectedInsuranceCompany
        {
            get => selectedInsuranceCompany;
            set
            {
                selectedInsuranceCompany = value;
                OnPropertyChanged();
            }
        }
        private string personalBank;
        public string PersonalBank
        {
            get => personalBank;
            set
            {
                personalBank = value;
                OnPropertyChanged();
            }
        }
        private string homeAdress;
        public string HomeAdress
        {
            get => homeAdress;
            set
            {
                homeAdress = value;
                OnPropertyChanged();
            }
        }
        private string socialSecurityNumber;
        public string SocialSecurityNumber
        {
            get => socialSecurityNumber;
            set
            {
                socialSecurityNumber = value;
                OnPropertyChanged();
            }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }
        private string contactPerson1;
        public string ContactPerson1
        {
            get => contactPerson1;
            set
            {
                contactPerson1 = value;
                OnPropertyChanged();
            }
        }
        private string contactPerson1PhoneNumber;
        public string ContactPerson1PhoneNumber
        {
            get => contactPerson1PhoneNumber;
            set
            {
                contactPerson1PhoneNumber = value;
                OnPropertyChanged();
            }
        }
        private string contactPerson2;
        public string ContactPerson2
        {
            get => contactPerson2;
            set
            {
                contactPerson2 = value;
                OnPropertyChanged();

            }

        }
        private string contactPerson2PhoneNumber;
        public string ContactPerson2PhoneNumber
        {
            get => contactPerson2PhoneNumber;
            set
            {
                contactPerson2PhoneNumber = value;
                OnPropertyChanged();
            }
        }


        public ICommand CreateUserCommand { get; }

        public CreateUserViewModel()
        {
            _repo = new UserRepositoryDb();
            CreateUserCommand = new Command(async () => await CreateUser());
        }

        private async Task CreateUser()
        {
            var existingUser = await _repo.GetUserByEmployeeId(EmployeeId);

            if (existingUser != null)
            {
                await MauiApp.Current.MainPage.DisplayAlertAsync("Error", "User already exists", "OK");
                return;
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId = employeeId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password),
                Email = Email,
                Role = UserRole.Employee,
                CreatedAt = DateTime.Now,
                Insurance = SelectedInsuranceCompany,
                HomeAdress = HomeAdress,
                PersonalBank = PersonalBank,
                PhoneNumber = PhoneNumber,
                ContactPerson1 = ContactPerson1,
                ContactPerson1PhoneNumber = ContactPerson1PhoneNumber,
                ContactPerson2 = ContactPerson2,
                ContactPerson2PhoneNumber = ContactPerson2PhoneNumber,
                SocialSecurityNumber = SocialSecurityNumber
            };

            await _repo.OnClickedCreateUser(user);

            await MauiApp.Current.MainPage.DisplayAlertAsync("Success", "User created!", "OK");
        }

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
