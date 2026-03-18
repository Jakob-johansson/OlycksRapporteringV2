using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.MAUI.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class LoggedInAdminPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ReportService _reportService;

        //STATISTIK\\
        private int _totalReports;
        public int TotalReports
        {
            get => _totalReports;
            set { _totalReports = value; OnPropertyChanged(); }
        }

        private int _pendingReports;
        public int PendingReports
        {
            get => _pendingReports;
            set { _pendingReports = value; OnPropertyChanged(); }
        }

        private int _approvedReports;
        public int ApprovedReports
        {
            get => _approvedReports;
            set { _approvedReports = value; OnPropertyChanged(); }
        }

        //KONSTRUKTOR\\
        public LoggedInAdminPageViewModel()
        {
            _reportService = new ReportService();
        }

        //HÄMTA STATISTIK\\
        public async Task LoadStats()
        {
            TotalReports = await _reportService.GetTotalReportCount();
            PendingReports = await _reportService.GetReportCountByStatus(ReportStatus.Pending);   // ← lägg till parameter
            ApprovedReports = await _reportService.GetReportCountByStatus(ReportStatus.Approved); // ← lägg till parameter
        }

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}