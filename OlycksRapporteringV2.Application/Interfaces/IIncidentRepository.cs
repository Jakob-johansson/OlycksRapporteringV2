using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

public interface IIncidentRepository
{
    Task CreateIncident(Incident incident);
    Task<List<Incident>> GetActiveIncidents();
    Task DeactivateIncident(string id);
}