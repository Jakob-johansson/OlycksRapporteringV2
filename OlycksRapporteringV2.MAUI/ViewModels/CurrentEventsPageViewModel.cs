using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OlycksRapporteringV2.MAUI.ViewModels
{
    public class CurrentEventsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IIncidentRepository _incidentRepo;

        //LISTOR\\
        public ObservableCollection<Incident> Incidents { get; set; } = new();
        public bool HasNoIncidents => !Incidents.Any();

        //KONSTRUKTOR\\
        public CurrentEventsPageViewModel()
        {
            _incidentRepo = new IncidentRepositoryDb();
        }

        //HÄMTA AKTIVA HÄNDELSER\\
        public async Task LoadIncidents()
        {
            var incidents = await _incidentRepo.GetActiveIncidents();

            //SORTERA EFTER PRIORITET — CRITICAL FÖRST\\
            var sorted = incidents
                .OrderByDescending(i => i.Priority)
                .ToList();

            Incidents.Clear();
            foreach (var incident in sorted)
                Incidents.Add(incident);

            OnPropertyChanged(nameof(HasNoIncidents));
        }

        //PRIORITETSFÄRG FÖR VARJE HÄNDELSE\\
        public static string GetPriorityColor(Priority priority) => priority switch
        {
            Priority.Critical => "#E83B3B",
            Priority.High => "#F5A623",
            Priority.Medium => "#4A9EFF",
            Priority.Low => "#4CAF50",
            _ => "#8A8F9E"
        };

        public void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
