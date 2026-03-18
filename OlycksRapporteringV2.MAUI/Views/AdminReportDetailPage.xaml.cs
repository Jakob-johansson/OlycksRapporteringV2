using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.MAUI.Services;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class AdminReportDetailPage : ContentPage
{
    public AdminReportDetailPage(Report report)
    {
        InitializeComponent();
        BindingContext = new AdminReportDetailPageViewModel(report);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    //SÄTT STATUS\\
    private async void OnSetCreatedClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.Created);

    private async void OnSetPendingClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.Pending);

    private async void OnSetUnderReviewClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.UnderReview);

    private async void OnSetApprovedClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as AdminReportDetailPageViewModel;

        //VÄLJ PRIORITET\\
        var priority = await DisplayActionSheet(
            "Välj prioritet",
            "Avbryt",
            null,
            "Low", "Medium", "High", "Critical");

        if (priority == "Avbryt" || priority == null) return;

        //SKRIV EN RUBRIK\\
        string title = await DisplayPromptAsync(
            "Händelserubrik",
            "Skriv en kort rubrik för händelsen:",
            placeholder: "t.ex. Risk för elbrand i hall C");

        if (string.IsNullOrWhiteSpace(title)) return;

        //SKRIV EN BESKRIVNING\\
        string description = await DisplayPromptAsync(
            "Beskrivning",
            "Kort beskrivning till användarna:",
            placeholder: "t.ex. Maskinen i hall B är ur funktion");

        if (string.IsNullOrWhiteSpace(description)) return;

        //NOTIFIERA ALLA ELLER SPECIFIKA\\
        var notifyChoice = await DisplayActionSheet(
            "Vem ska se händelsen?",
            "Avbryt",
            null,
            "Alla användare",
            "Specifika användare");

        if (notifyChoice == "Avbryt" || notifyChoice == null) return;

        var priorityEnum = priority switch
        {
            "Low" => Priority.Low,
            "Medium" => Priority.Medium,
            "High" => Priority.High,
            "Critical" => Priority.Critical,
            _ => Priority.Low
        };

        await vm.ApproveWithIncident(
            priorityEnum,
            title,
            description,
            notifyChoice == "Alla användare");
    }

    private async void OnSetRejectedClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.Rejected);



    private async void OnDownloadPdfClicked(object sernder, EventArgs e)
    {
        var vm = BindingContext as AdminReportDetailPageViewModel;
        var pdfService = new PdfService();
        try
        {
            await DisplayAlertAsync("Generar", "Skapar PDF...", "OK");
            var path = await pdfService.SavePdf(vm.Report);

            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(path),
                Title = "Öppna rapport"
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Fel", $"Kunde inte generera PDF: {ex.Message}", "OK");
        }
    }

}
