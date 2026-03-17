using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
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
    public class ShowAccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IUserRepository _repo;

        // ── Fältvärden ────────────────────────────────────────────────

        private string employeeId;
        public string EmployeeId { get => employeeId; set { employeeId = value; OnPropertyChanged(); } }

        private string email;
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }

        private string phoneNumber;
        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; OnPropertyChanged(); } }

        private string homeAdress;
        public string HomeAdress { get => homeAdress; set { homeAdress = value; OnPropertyChanged(); } }

        private string personalBank;
        public string PersonalBank { get => personalBank; set { personalBank = value; OnPropertyChanged(); } }

        private string insurance;
        public string Insurance { get => insurance; set { insurance = value; OnPropertyChanged(); } }

        private string contactPerson1;
        public string ContactPerson1 { get => contactPerson1; set { contactPerson1 = value; OnPropertyChanged(); } }

        private string contactPerson1PhoneNumber;
        public string ContactPerson1PhoneNumber { get => contactPerson1PhoneNumber; set { contactPerson1PhoneNumber = value; OnPropertyChanged(); } }

        private string contactPerson2;
        public string ContactPerson2 { get => contactPerson2; set { contactPerson2 = value; OnPropertyChanged(); } }

        private string contactPerson2PhoneNumber;
        public string ContactPerson2PhoneNumber { get => contactPerson2PhoneNumber; set { contactPerson2PhoneNumber = value; OnPropertyChanged(); } }

        // ── ReadOnly/Editing-lägen ────────────────────────────────────

        private bool employeeIdReadOnly = true;
        public bool EmployeeIdReadOnly { get => employeeIdReadOnly; set { employeeIdReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(EmployeeIdEditing)); } }
        public bool EmployeeIdEditing => !employeeIdReadOnly;

        private bool emailReadOnly = true;
        public bool EmailReadOnly { get => emailReadOnly; set { emailReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(EmailEditing)); } }
        public bool EmailEditing => !emailReadOnly;

        private bool phoneReadOnly = true;
        public bool PhoneReadOnly { get => phoneReadOnly; set { phoneReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(PhoneEditing)); } }
        public bool PhoneEditing => !phoneReadOnly;

        private bool homeAdressReadOnly = true;
        public bool HomeAdressReadOnly { get => homeAdressReadOnly; set { homeAdressReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(HomeAdressEditing)); } }
        public bool HomeAdressEditing => !homeAdressReadOnly;

        private bool personalBankReadOnly = true;
        public bool PersonalBankReadOnly { get => personalBankReadOnly; set { personalBankReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(PersonalBankEditing)); } }
        public bool PersonalBankEditing => !personalBankReadOnly;

        private bool insuranceReadOnly = true;
        public bool InsuranceReadOnly { get => insuranceReadOnly; set { insuranceReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(InsuranceEditing)); } }
        public bool InsuranceEditing => !insuranceReadOnly;

        private bool contactPerson1ReadOnly = true;
        public bool ContactPerson1ReadOnly { get => contactPerson1ReadOnly; set { contactPerson1ReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(ContactPerson1Editing)); } }
        public bool ContactPerson1Editing => !contactPerson1ReadOnly;

        private bool contactPerson1PhoneReadOnly = true;
        public bool ContactPerson1PhoneReadOnly { get => contactPerson1PhoneReadOnly; set { contactPerson1PhoneReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(ContactPerson1PhoneEditing)); } }
        public bool ContactPerson1PhoneEditing => !contactPerson1PhoneReadOnly;

        private bool contactPerson2ReadOnly = true;
        public bool ContactPerson2ReadOnly { get => contactPerson2ReadOnly; set { contactPerson2ReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(ContactPerson2Editing)); } }
        public bool ContactPerson2Editing => !contactPerson2ReadOnly;

        private bool contactPerson2PhoneReadOnly = true;
        public bool ContactPerson2PhoneReadOnly { get => contactPerson2PhoneReadOnly; set { contactPerson2PhoneReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(ContactPerson2PhoneEditing)); } }
        public bool ContactPerson2PhoneEditing => !contactPerson2PhoneReadOnly;

        // ── Save per fält (stänger edit-läge) ────────────────────────

        public ICommand SaveEmployeeIdCommand => new Command(() => EmployeeIdReadOnly = true);
        public ICommand SaveEmailCommand => new Command(() => EmailReadOnly = true);
        public ICommand SavePhoneCommand => new Command(() => PhoneReadOnly = true);
        public ICommand SaveHomeAdressCommand => new Command(() => HomeAdressReadOnly = true);
        public ICommand SavePersonalBankCommand => new Command(() => PersonalBankReadOnly = true);
        public ICommand SaveInsuranceCommand => new Command(() => InsuranceReadOnly = true);
        public ICommand SaveContactPerson1Command => new Command(() => ContactPerson1ReadOnly = true);
        public ICommand SaveContactPerson1PhoneCommand => new Command(() => ContactPerson1PhoneReadOnly = true);
        public ICommand SaveContactPerson2Command => new Command(() => ContactPerson2ReadOnly = true);
        public ICommand SaveContactPerson2PhoneCommand => new Command(() => ContactPerson2PhoneReadOnly = true);

        // ── Spara allt till databasen ─────────────────────────────────

        public ICommand SaveAllCommand => new Command(async () =>
        {
            var user = UserSession.Instance.CurrentUser;
            user.EmployeeId = EmployeeId;
            user.Email = Email;
            user.PhoneNumber = PhoneNumber;
            user.HomeAdress = HomeAdress;
            user.PersonalBank = PersonalBank;
            user.ContactPerson1 = ContactPerson1;
            user.ContactPerson1PhoneNumber = ContactPerson1PhoneNumber;
            user.ContactPerson2 = ContactPerson2;
            user.ContactPerson2PhoneNumber = ContactPerson2PhoneNumber;

            await _repo.UpdateUser(user);
            UserSession.Instance.StartSession(user); // uppdatera sessionen

            await MauiApp.Current.MainPage.DisplayAlert("Sparat", "Dina uppgifter har uppdaterats.", "OK");
        });

        // ── ToggleEdit ────────────────────────────────────────────────

        public void ToggleEdit(string field)
        {
            switch (field)
            {
                case "EmployeeId": EmployeeIdReadOnly = !EmployeeIdReadOnly; break;
                case "Email": EmailReadOnly = !EmailReadOnly; break;
                case "Phone": PhoneReadOnly = !PhoneReadOnly; break;
                case "HomeAdress": HomeAdressReadOnly = !HomeAdressReadOnly; break;
                case "PersonalBank": PersonalBankReadOnly = !PersonalBankReadOnly; break;
                case "Insurance": InsuranceReadOnly = !InsuranceReadOnly; break;
                case "ContactPerson1": ContactPerson1ReadOnly = !ContactPerson1ReadOnly; break;
                case "ContactPerson1Phone": ContactPerson1PhoneReadOnly = !ContactPerson1PhoneReadOnly; break;
                case "ContactPerson2": ContactPerson2ReadOnly = !ContactPerson2ReadOnly; break;
                case "ContactPerson2Phone": ContactPerson2PhoneReadOnly = !ContactPerson2PhoneReadOnly; break;
            }
        }

        // ── Konstruktor ───────────────────────────────────────────────

        public ShowAccountViewModel()
        {
            _repo = new UserRepositoryDb();
            var user = UserSession.Instance.CurrentUser;
            if (user != null)
            {
                EmployeeId = user.EmployeeId;
                Email = user.Email;
                PhoneNumber = user.PhoneNumber;
                HomeAdress = user.HomeAdress;
                PersonalBank = user.PersonalBank;
                Insurance = user.Insurance.ToString();
                ContactPerson1 = user.ContactPerson1;
                ContactPerson1PhoneNumber = user.ContactPerson1PhoneNumber;
                ContactPerson2 = user.ContactPerson2;
                ContactPerson2PhoneNumber = user.ContactPerson2PhoneNumber;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
