using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Models;
using OlycksRapporteringV2.MAUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class AdminArchivePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // DESIGN PATTERN: FACADE
        private readonly ReportService _reportService;
        private readonly IUserRepository _userRepo;

        //LISTOR\\
        public ObservableCollection<SelectableReport> Reports { get; set; } = new();
        public bool HasNoReports => !Reports.Any();

        //VÄLJ-LÄGE\\
        private bool _selectModeActive;
        public bool SelectModeActive
        {
            get => _selectModeActive;
            set { _selectModeActive = value; OnPropertyChanged(); }
        }

        //KONSTRUKTOR\\
        public AdminArchivePageViewModel()
        {
            _reportService = new ReportService();
            _userRepo = new UserRepositoryDb();
        }

        //HÄMTA ARKIVERADE RAPPORTER\\
        public async Task LoadReports()
        {
            var reports = await _reportService.GetArchivedReports();
            Reports.Clear();
            foreach (var report in reports)
            {
                var user = await _userRepo.GetUserById(report.CreatedByUserId);
                Reports.Add(new SelectableReport
                {
                    Report = report,
                    CreatedByName = user != null ? user.EmployeeId : "Okänd användare"
                });
            }
            OnPropertyChanged(nameof(HasNoReports));
        }

        //VÄXLA MARKERING\\
        public void ToggleSelection(SelectableReport item)
        {
            item.IsSelected = !item.IsSelected;
        }

        //AVMARKERA ALLA\\
        public void ClearSelections()
        {
            foreach (var item in Reports)
                item.IsSelected = false;
        }

        //RADERA MARKERADE\\
        public async Task DeleteSelected()
        {
            var toDelete = Reports.Where(r => r.IsSelected).ToList();
            foreach (var item in toDelete)
            {
                await _reportService.DeleteReport(item.Report.Id);
                Reports.Remove(item);
            }
            SelectModeActive = false;
            OnPropertyChanged(nameof(HasNoReports));
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
