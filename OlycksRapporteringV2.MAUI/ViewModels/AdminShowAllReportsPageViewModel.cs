using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Models;
using OlycksRapporteringV2.MAUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class AdminShowAllReportsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //Design patter: Facade
        //Här använder vi ReportService.cs för att hantera allting med repositories. 
        //Då behöver inte den här klassen veta om allt det.



        private readonly ReportService _reportService;
        private readonly IUserRepository _userRepo;

        //ALLA RAPPORTER (ANVÄNDS FÖR FILTRERING)\\
        private List<SelectableReport> _allReports = new();

        public ObservableCollection<SelectableReport> Reports { get; set; } = new();
        public bool HasNoReports => !Reports.Any();

        private string _activeFilter;
        public string ActiveFilter
        {
            get => _activeFilter;
            set
            {
                _activeFilter = value;
                OnPropertyChanged();
            }
        }


        //VÄLJ-LÄGE\\
        private bool _selectModeActive;
        public bool SelectModeActive
        {
            get => _selectModeActive;
            set { _selectModeActive = value; OnPropertyChanged(); }
        }

        //VALD RAPPORT\\
        private Report _selectedReport;
        public Report SelectedReport
        {
            get => _selectedReport;
            set { _selectedReport = value; OnPropertyChanged(); }
        }

        //KONSTRUKTOR\\
        public AdminShowAllReportsPageViewModel(string startFilter = null)
        {
            _reportService = new ReportService();
            _userRepo = new UserRepositoryDb();
            _startFilter = startFilter;
        }
        private string _startFilter;
        //HÄMTA ALLA RAPPORTER\\
        public async Task LoadReports()
        {
            var reports = await _reportService.GetAllReports();
            _allReports.Clear();
            foreach (var report in reports)
            {
                var user = await _userRepo.GetUserById(report.CreatedByUserId);
                _allReports.Add(new SelectableReport
                {
                    Report = report,
                    CreatedByName = user != null ? user.EmployeeId : "Okänd användare"
                });
            }
            ApplyFilter(_startFilter); // VISA ALLA FRÅN START\\
        }

        //VÄXLA MARKERING\\
        public void ToggleSelection(SelectableReport item)
        {
            item.IsSelected = !item.IsSelected;
        }

        //RADERA MARKERADE\\
        public async Task DeleteSelected()
        {
            var toDelete = Reports.Where(r => r.IsSelected).ToList();
            foreach (var item in toDelete)
            {
                await _reportService.DeleteReport(item.Report.Id);
                _allReports.Remove(item);
                Reports.Remove(item);
            }
            SelectModeActive = false;
            SelectedReport = null;
            OnPropertyChanged(nameof(HasNoReports));
        }

        //AVMARKERA ALLA\\
        public void ClearSelections()
        {
            foreach (var item in Reports)
                item.IsSelected = false;
        }

        //FILTRERA PÅ STATUS\\
        public void FilterByStatus(string status)
        {
            ApplyFilter(status);
        }

        private void ApplyFilter(string status)
        {
            ActiveFilter = status ?? "";

            Reports.Clear();
            var filtered = status == null
                ? _allReports
                : _allReports.Where(r => r.Report.Status.ToString() == status).ToList();

            foreach (var item in filtered)
                Reports.Add(item);

            OnPropertyChanged(nameof(HasNoReports));
        }

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}