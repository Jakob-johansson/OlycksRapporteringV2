
using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.MAUI.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiApp = Microsoft.Maui.Controls.Application;

namespace OlycksRapporteringV2.MAUI.ViewModels;

public class CreateReportViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly ReportService _reportService;
    private readonly WeatherService _weatherService;

    //Design pattern: Facade
    //Här använder vi facade för att viewmodel ska slippa hålla koll på alla repositories som behövs, nu används ReportService istället.


    //KONTROLLERAR OM NÅGOT FÄLLT ÄR FEL\\
    //---Skickar sedan ett felmeddelande---\\
    public bool HasAnyError => _errors.ContainsValue(true);
    //FELHANTERING DICTIONARY\\
    private Dictionary<string, bool> _errors = new();

    public bool SelectedAccidentTypeHasError
    {
        get => _errors.GetValueOrDefault("SelectedAccidentType");
        set 
        { 
            _errors["SelectedAccidentType"] = value; 
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasAnyError));
        }
    }
    public bool ReportTitleHasError
    {
        get => _errors.GetValueOrDefault("ReportTitle");
        set 
        { _errors["ReportTitle"] = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasAnyError));
        }
    }
    public bool ReportDescriptionHasError
    {
        get => _errors.GetValueOrDefault("ReportDescription");
        set 
        { _errors["ReportDescription"] = value; 
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasAnyError));
        }
    }
    public bool SelectedAffectedPersonTypeHasError
    {
        get => _errors.GetValueOrDefault("SelectedAffectedPersonType");
        set 
        { _errors["SelectedAffectedPersonType"] = value; 
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasAnyError));
        }
    }
    public bool LocationHasError
    {
        get => _errors.GetValueOrDefault("Location");
        set 
        { _errors["Location"] = value; 
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasAnyError));
        }
    }
    public bool TimeHasError
    {
        get => _errors.GetValueOrDefault("Time");
        set 
        { _errors["Time"] = value; 
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasAnyError));
        }
    }

    //PROPERTIES\\
    private string reportTitle;
    public string ReportTitle
    {
        get => reportTitle;
        set { reportTitle = value; OnPropertyChanged(); ReportTitleHasError = false; }
    }

    private string reportDescription;
    public string ReportDescription
    {
        get => reportDescription;
        set { reportDescription = value; OnPropertyChanged(); ReportDescriptionHasError = false; }
    }

    private string peopleInvolved;
    public string PeopleInvolved
    {
        get => peopleInvolved;
        set { peopleInvolved = value; OnPropertyChanged(); }
    }

    private string location;
    public string Location
    {
        get => location;
        set { location = value; OnPropertyChanged(); LocationHasError = false; }
    }

    private string timeOfAccident;
    public string TimeOfAccident
    {
        get => timeOfAccident;
        set { timeOfAccident = value; OnPropertyChanged(); TimeHasError = false; }
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

    private string whoReportIsAbout;
    public string WhoReportIsAbout
    {
        get => whoReportIsAbout;
        set { whoReportIsAbout = value; OnPropertyChanged(); }
    }

    //LISTOR FÖR PICKER\\
    public List<string> AccidentTypes { get; set; } = new List<string>
        {
            "Fallolycka", "Fordonsincident", "Maskinskada",
            "Kemikalieolycka", "Brand", "Elolycka/Elfel", "Personskada", "Annat"
        };

    public List<string> AffectedPersonType { get; set; } = new List<string>
        {
            "Mig själv", "Anställd", "Utestående person",
            "Patient", "Besökare", "Entreprenör/konsult", "Övrigt"
        };

    private string selectedAccidentType;
    public string SelectedAccidentType
    {
        get => selectedAccidentType;
        set { selectedAccidentType = value; OnPropertyChanged(); SelectedAccidentTypeHasError = false; }
    }

    private string selectedAffectedPersonType;
    public string SelectedAffectedPersonType
    {
        get => selectedAffectedPersonType;
        set { selectedAffectedPersonType = value; OnPropertyChanged(); SelectedAffectedPersonTypeHasError = false; }
    }

    //KOMMANDO\\
    public ICommand CreateReportCommand { get; }

    public CreateReportViewModel()
    {
        _reportService = new ReportService();
        _weatherService = new WeatherService();
        CreateReportCommand = new Command(async () => await CreateReport());

    }

    //------SKAPA RAPPORT MED FELHANTERING-------\\
    //  Samt hämta data för väder \\
    private async Task CreateReport()
    {
        bool hasErrors = false;

        if (string.IsNullOrEmpty(SelectedAccidentType))
        { SelectedAccidentTypeHasError = true; hasErrors = true; }

        if (string.IsNullOrWhiteSpace(ReportTitle) || ReportTitle.Length < 5)
        { ReportTitleHasError = true; hasErrors = true; }

        if (string.IsNullOrWhiteSpace(ReportDescription) || ReportDescription.Length < 10)
        { ReportDescriptionHasError = true; hasErrors = true; }

        if (string.IsNullOrEmpty(SelectedAffectedPersonType))
        { SelectedAffectedPersonTypeHasError = true; hasErrors = true; }

        if (string.IsNullOrWhiteSpace(Location))
        { LocationHasError = true; hasErrors = true; }

        //VALIDERING AV DATUM OCH TID\\
        var formats = new[] { "yyyy-MM-dd HH:mm", "yyyyMMdd HH:mm", "d/M-yy HH:mm" };
        bool timeOk = DateTime.TryParseExact(
            TimeOfAccident?.Trim(),
            formats,
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime parsedTime);

       


        if (!timeOk)
        { TimeHasError = true; hasErrors = true; }
        //Väder
       


        //OM DET FINNS FEL, STOPPA HÄR\\
        if (hasErrors) return;

        //Hämta väder location samt tid för olycka. 
        var weather = await _weatherService.GetWeatherForLocation(Location, TimeOfAccident);
        
        //Felsökning, skickar ut i output fönstret. ej viktigt för programmet med viktigt för mig :D
        System.Diagnostics.Debug.WriteLine($"Väder resultat: {weather?.description}, {weather?.temp}, {weather?.wind}");
        System.Diagnostics.Debug.WriteLine($"Weather är null: {weather == null}");
        //SKAPA RAPPORT OCH SPARA I DATABASEN\\
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
            CreatedByUserId = UserSession.Instance.CurrentUser.Id,
            CreatedAt = DateTime.Now,
            Status = ReportStatus.Created,
            IsVisibleToUser = true,
            Insurance = UserSession.Instance.CurrentUser.Insurance,
            TimeOfAccident = TimeOfAccident,
            AffectedPersonType = SelectedAffectedPersonType,
            WeatherDescription = weather?.description,
            Temperature = weather?.temp,
            WindSpeed = weather?.wind,
            WeatherLocation = Location



        };
       
      

        await _reportService.CreateReport(report);
        

        await MauiApp.Current.MainPage.DisplayAlert("Success", "Rapport skapad!", "OK");
    }

    //NOTIFIERING AV ÄNDRINGAR\\
    void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}