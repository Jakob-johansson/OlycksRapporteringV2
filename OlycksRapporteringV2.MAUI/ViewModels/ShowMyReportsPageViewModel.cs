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
    public class ShowMyReportsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ReportService _reportService;
        private readonly INotificationRepository _notificationRepo; 

       
        public ObservableCollection<SelectableReport> Reports { get; set; } = new();
        public bool HasNoReports => !Reports.Any();

      
        private bool _selectModeActive;
        public bool SelectModeActive
        {
            get => _selectModeActive;
            set { _selectModeActive = value; OnPropertyChanged(); }
        }

       
        private Report _selectedReport;
        public Report SelectedReport
        {
            get => _selectedReport;
            set { _selectedReport = value; OnPropertyChanged(); }
        }

       
        public ShowMyReportsPageViewModel()
        {
            _reportService = new ReportService();
            _notificationRepo = new NotificationRepositoryDb();
        }

       
        public async Task LoadReports()
        {
            var reports = await _reportService.GetMyReports();
            Reports.Clear();
            foreach (var report in reports)
                Reports.Add(new SelectableReport { Report = report });
            OnPropertyChanged(nameof(HasNoReports));
        }

        
        public void ToggleSelection(SelectableReport item)
        {
            item.IsSelected = !item.IsSelected;
        }

        
        public void ClearSelections()
        {
            foreach (var item in Reports)
                item.IsSelected = false;
        }

        
        public async Task DeleteSelected()
        {
            var toDelete = Reports.Where(r => r.IsSelected).ToList();
            foreach (var item in toDelete)
            {
                await _reportService.DeleteReport(item.Report.Id);
                Reports.Remove(item);
            }
            SelectModeActive = false;
            SelectedReport = null;
            OnPropertyChanged(nameof(HasNoReports));
        }

        
        public async Task SendEditRequest(Report report)
        {
            
            await _reportService.RequestEdit(report);
        }

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}