using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Infrastructure.Repositories;
using OlycksRapporteringV2.MAUI.Models;
using OlycksRapporteringV2.MAUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class ShowMyReportsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IReportRepository _repo;

        public void ClearSelections()
        {
            foreach (var item in Reports)
                item.IsSelected = false;
        }
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

        //VALD RAPPORT FÖR REDIGERING\\
        private Report _selectedReport;
        public Report SelectedReport
        {
            get => _selectedReport;
            set { _selectedReport = value; OnPropertyChanged(); }
        }

        //KONSTRUKTOR\\
        public ShowMyReportsPageViewModel()
        {
            _repo = new ReportRepositoryDb();
        }

        //HÄMTA RAPPORTER FRÅN DATABASEN\\
        public async Task LoadReports()
        {
            var reports = await _repo.GetReportByUserId(UserSession.CurrentUser.Id);
            Reports.Clear();
            foreach (var report in reports)
                Reports.Add(new SelectableReport { Report = report });
            OnPropertyChanged(nameof (HasNoReports));
            
        }

        //VÄXLA MARKERING PÅ ETT KORT\\
        public void ToggleSelection(SelectableReport item)
        {
            item.IsSelected = !item.IsSelected;
        }

        //RADERA MARKERADE RAPPORTER\\
        public async Task DeleteSelected()
        {
            var toDelete = Reports.Where(r => r.IsSelected).ToList();
            foreach (var item in toDelete)
            {
                await _repo.DeleteReport(item.Report.Id);
                Reports.Remove(item);
            }
            SelectModeActive = false;
            SelectedReport = null; //Gör så man inte kan "redigera" nått som inte finns
            OnPropertyChanged(nameof(HasNoReports));
        }

        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
