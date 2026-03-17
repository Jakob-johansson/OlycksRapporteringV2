using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiApp = Microsoft.Maui.Controls.Application;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class AdminReportDetailPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //Design patter: Facade
        //Här använder vi ReportService.cs för att denna klass inte behöver känna till alla Repositories.
        private readonly ReportService _reportService;
        



        //RAPPORTEN\\
        private Report _report;
        public Report Report
        {
            get => _report;
            set { _report = value; OnPropertyChanged(); }
        }

        //AKTIV STATUS (FÖR ATT MARKERA VALD KNAPP)\\
        private ReportStatus _activeStatus;
        public ReportStatus ActiveStatus
        {
            get => _activeStatus;
            set { _activeStatus = value; OnPropertyChanged(); }
        }

        //KONSTRUKTOR\\
        public AdminReportDetailPageViewModel(Report report)
        {
            _reportService = new ReportService();
            Report = report;
            ActiveStatus = report.Status;
        }

        //SÄTT STATUS\\
        public async Task SetStatus(ReportStatus status)
        {
            await _reportService.UpdateReportStatus(Report.Id, status);
            Report.Status = status;
            ActiveStatus = status;
            OnPropertyChanged(nameof(Report));
            await MauiApp.Current.MainPage.DisplayAlert(
                "Status uppdaterad",
                $"Rapporten är nu markerad som: {status}",
                "OK");
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}