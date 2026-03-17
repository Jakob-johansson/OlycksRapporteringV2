using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiApp = Microsoft.Maui.Controls.Application;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class EditReportPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ReportService _reportService;

       
        private Dictionary<string, bool> _errors = new();

        
        private Report _report;

        
        public bool TimeHasError
        {
            get => _errors.GetValueOrDefault("Time");
            set { _errors["Time"] = value; OnPropertyChanged(); }
        }

       
        private string reportTitle;
        public string ReportTitle
        {
            get => reportTitle;
            set { reportTitle = value; OnPropertyChanged(); }
        }

        private string reportDescription;
        public string ReportDescription
        {
            get => reportDescription;
            set { reportDescription = value; OnPropertyChanged(); }
        }

        private string location;
        public string Location
        {
            get => location;
            set { location = value; OnPropertyChanged(); }
        }

        private string timeOfAccident;
        public string TimeOfAccident
        {
            get => timeOfAccident;
            set { timeOfAccident = value; OnPropertyChanged(); TimeHasError = false; }
        }

        private string peopleInvolved;
        public string PeopleInvolved
        {
            get => peopleInvolved;
            set { peopleInvolved = value; OnPropertyChanged(); }
        }

        private string materialDamage;
        public string MaterialDamage
        {
            get => materialDamage;
            set { materialDamage = value; OnPropertyChanged(); }
        }

        private string immediateActions;
        public string ImmediateActions
        {
            get => immediateActions;
            set { immediateActions = value; OnPropertyChanged(); }
        }

        private string preventiveActions;
        public string PreventiveActions
        {
            get => preventiveActions;
            set { preventiveActions = value; OnPropertyChanged(); }
        }

        
        private bool reportTitleReadOnly = true;
        public bool ReportTitleReadOnly { get => reportTitleReadOnly; set { reportTitleReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(ReportTitleEditing)); } }
        public bool ReportTitleEditing => !reportTitleReadOnly;

        private bool reportDescriptionReadOnly = true;
        public bool ReportDescriptionReadOnly { get => reportDescriptionReadOnly; set { reportDescriptionReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(ReportDescriptionEditing)); } }
        public bool ReportDescriptionEditing => !reportDescriptionReadOnly;

        private bool locationReadOnly = true;
        public bool LocationReadOnly { get => locationReadOnly; set { locationReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(LocationEditing)); } }
        public bool LocationEditing => !locationReadOnly;

        private bool timeReadOnly = true;
        public bool TimeReadOnly { get => timeReadOnly; set { timeReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(TimeEditing)); } }
        public bool TimeEditing => !timeReadOnly;

        private bool peopleReadOnly = true;
        public bool PeopleReadOnly { get => peopleReadOnly; set { peopleReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(PeopleEditing)); } }
        public bool PeopleEditing => !peopleReadOnly;

        private bool materialReadOnly = true;
        public bool MaterialReadOnly { get => materialReadOnly; set { materialReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(MaterialEditing)); } }
        public bool MaterialEditing => !materialReadOnly;

        private bool immediateReadOnly = true;
        public bool ImmediateReadOnly { get => immediateReadOnly; set { immediateReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(ImmediateEditing)); } }
        public bool ImmediateEditing => !immediateReadOnly;

        private bool preventiveReadOnly = true;
        public bool PreventiveReadOnly { get => preventiveReadOnly; set { preventiveReadOnly = value; OnPropertyChanged(); OnPropertyChanged(nameof(PreventiveEditing)); } }
        public bool PreventiveEditing => !preventiveReadOnly;

        
        public ICommand SaveReportTitleCommand => new Command(() => ReportTitleReadOnly = true);
        public ICommand SaveReportDescriptionCommand => new Command(() => ReportDescriptionReadOnly = true);
        public ICommand SaveLocationCommand => new Command(() => LocationReadOnly = true);
        public ICommand SaveTimeCommand => new Command(() => TimeReadOnly = true);
        public ICommand SavePeopleCommand => new Command(() => PeopleReadOnly = true);
        public ICommand SaveMaterialCommand => new Command(() => MaterialReadOnly = true);
        public ICommand SaveImmediateCommand => new Command(() => ImmediateReadOnly = true);
        public ICommand SavePreventiveCommand => new Command(() => PreventiveReadOnly = true);

     
        public void ToggleEdit(string field)
        {
            switch (field)
            {
                case "ReportTitle": ReportTitleReadOnly = !ReportTitleReadOnly; break;
                case "ReportDescription": ReportDescriptionReadOnly = !ReportDescriptionReadOnly; break;
                case "Location": LocationReadOnly = !LocationReadOnly; break;
                case "Time": TimeReadOnly = !TimeReadOnly; break;
                case "People": PeopleReadOnly = !PeopleReadOnly; break;
                case "Material": MaterialReadOnly = !MaterialReadOnly; break;
                case "Immediate": ImmediateReadOnly = !ImmediateReadOnly; break;
                case "Preventive": PreventiveReadOnly = !PreventiveReadOnly; break;
            }
        }

      
        public ICommand SaveAllCommand => new Command(async () =>
        {
            var formats = new[] { "yyyy-MM-dd HH:mm", "yyyyMMdd HH:mm", "d/M-yy HH:mm" };
            bool timeOk = DateTime.TryParseExact(
                TimeOfAccident?.Trim(), formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out _);

            if (!timeOk)
            {
                TimeHasError = true;
                return;
            }

            //UPPDATERA RAPPORTEN\\
            _report.ReportTitle = ReportTitle;
            _report.ReportDescription = ReportDescription;
            _report.Location = Location;
            _report.TimeOfAccident = TimeOfAccident;
            _report.PeopleInvolved = PeopleInvolved;
            _report.MaterialDamage = MaterialDamage;
            _report.ImmediateActions = ImmediateActions;
            _report.PreventiveActions = PreventiveActions;

            await _reportService.UpdateReport(_report);
            await MauiApp.Current.MainPage.DisplayAlert("Sparat", "Rapporten har uppdaterats.", "OK");
        });

      
        public EditReportPageViewModel(Report report)
        {
            _reportService = new ReportService();
            _report = report;

            
            ReportTitle = report.ReportTitle;
            ReportDescription = report.ReportDescription;
            Location = report.Location;
            TimeOfAccident = report.TimeOfAccident;
            PeopleInvolved = report.PeopleInvolved;
            MaterialDamage = report.MaterialDamage;
            ImmediateActions = report.ImmediateActions;
            PreventiveActions = report.PreventiveActions;
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
