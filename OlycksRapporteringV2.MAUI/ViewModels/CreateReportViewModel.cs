using OlycksRapporteringV2.Application.Interfaces;
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

public class CreateReportViewModel : INotifyPropertyChanged
{
    private readonly IReportRepository _repo;

    public event PropertyChangedEventHandler? PropertyChanged;



    private string reportTitle;
    public string ReportTitle
    {
        get => reportTitle;
        set
        {
            reportTitle = value;
            OnPropertyChanged();
        }
    }

    private string reportDescription;
    public string ReportDescription
    {
        get => reportDescription;
        set
        {
            reportDescription = value;
            OnPropertyChanged();
        }
    }
    private string peopleInvolved;
    public string PeopleInvolved
    {
        get => peopleInvolved;
        set
        {
            peopleInvolved = value;
            OnPropertyChanged();
        }
    }

    private string location;
    public string Location
    {
        get => location;
        set
        {
            location = value;
            OnPropertyChanged();
        }
    }

    public List<string> AccidentTypes { get; set; } = new List<string>
    {
        "Fallolycka",
        "Fordonsincident",
        "Maskinskada",
        "Kemikalieolycka",
        "Brand",
        "Elolycka/Elfel",
        "Personskada",
        "Annat"
    };

    private string selectedAccidentType;
    public string SelectedAccidentType
    {
        get => selectedAccidentType;
        set
        {
            selectedAccidentType = value;
            OnPropertyChanged();
        }
    }
    private string materialDamage;
    public string MaterialDamage
    {
        get => materialDamage;
        set
        {
            materialDamage = value;
            OnPropertyChanged();
        }

    }
    private string immediateActions;
    public string ImmediateActions
    {
        get => immediateActions;
        set
        {
            immediateActions = value;
            OnPropertyChanged();
        }
    }

    private string preventiveActions;
    public string PreventiveActions
    {
        get => preventiveActions;
        set
        {
            preventiveActions = value;
            OnPropertyChanged();
        }
    }
    private string timeOfAccident;
    public string TimeOfAccident
    {
        get => timeOfAccident;
        set
        {
            timeOfAccident = value;
            OnPropertyChanged();
        }
    }

    public ICommand CreateReportCommand { get; }

    public CreateReportViewModel()
    {
        _repo = new ReportRepositoryDb();
        CreateReportCommand = new Command(async () => await CreateReport());
    }

    private async Task CreateReport()
    {
        var report = new Report
        {
            Id = Guid.NewGuid().ToString(),
            ReportTitle = ReportTitle,
            ReportDescription = ReportDescription,
            AccidentType = SelectedAccidentType,
            PeopleInvolved = PeopleInvolved,
            Location = Location,
            MaterialDamage = MaterialDamage,
            ImmediateActions = ImmediateActions,
            PreventiveActions = PreventiveActions,
            CreatedByUserId = UserSession.CurrentUser.Id,
            CreatedAt = DateTime.Now,
            Status = ReportStatus.Created,
            IsVisibleToUser = true,
            Insurance = UserSession.CurrentUser.Insurance,
            TimeOfAccident = TimeOfAccident
        };

        await _repo.CreateReport(report);

        await Application.Current.MainPage.DisplayAlert("Success", "Report created!", "OK");
    }

    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
