using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.MAUI.Models;
using OlycksRapporteringV2.MAUI.Services;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class ShowMyReportsPage : ContentPage
{
    public ShowMyReportsPage()
    {
        InitializeComponent();
        BindingContext = new ShowMyReportsPageViewModel();
    }



    //OnAppearing ANVÄNDS FÖR ATT LADDA RAPPORTEN VARJE GÅNG SIDAN VISAS\\
    //INTE BARA FÖRSTA GÅNGEN\\ 
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as ShowMyReportsPageViewModel).LoadReports();
    }
    private async void OnBackClicked(object sender, EventArgs e) =>
    await Navigation.PopAsync();
  
    private void OnSelectModeClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as ShowMyReportsPageViewModel;
        vm.SelectModeActive ^= true;

        if (!vm.SelectModeActive)
            vm.ClearSelections();
    }
       

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as ShowMyReportsPageViewModel;
        if (vm.SelectedReport == null)
        {
            await DisplayAlertAsync("Ingen vald", "Tryck på en rapport först", "OK");
            return;
        }
       if(vm.SelectedReport.Status == Domain.Enums.ReportStatus.Created)
        {
            await Navigation.PushAsync(new EditReportPage(vm.SelectedReport));
        }
        else
        {
            bool confirm = await DisplayAlertAsync(
                "Begär ändring",
                "Rapporten har redan granskats. vill du skicka en begäran till admin om att få ändra den?",
                "Ja", "Avbryt"
                );
            if (confirm)
            {
                await vm.SendEditRequest(vm.SelectedReport);
                await DisplayAlertAsync("Skickat", "Din begäran har skickats till admin", "Ok");
            }
        }
    }

    private async void OnDeleteSelectedClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as ShowMyReportsPageViewModel;
        var selected = vm.Reports.Where(r => r.IsSelected).ToList();
        if (!selected.Any())
        {
            await DisplayAlertAsync("Ingen vald", "Välj minst en rapport.", "OK");
            return;
        }

        bool confirm = await DisplayAlertAsync("Radera", $"Är du säker på att du vill reader {selected.Count} rapport(er)?", "Ja", "Avbryt");
        if (confirm)
            await vm.DeleteSelected();
    }

    private void OnReportTapped(object sender, TappedEventArgs e)
    {
        var vm = BindingContext as ShowMyReportsPageViewModel;
        var border = sender as Border;
        var item = border.BindingContext as SelectableReport;

        if (item == null) return;

        if (vm.SelectModeActive)
            vm.ToggleSelection(item);
        else 
        {
            if (vm.SelectedReport == item.Report)
                vm.SelectedReport = null;
            else
                vm.SelectedReport = item.Report;
        }
            
    }

    private async void OnDownloadPdfClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.BindingContext as SelectableReport;

        if (item == null) return;
        var pdfService = new PdfService();
        try
        {
            await DisplayAlertAsync("Genererar", "Skapar PDF...", "OK");
            var path = await pdfService.SavePdf(item.Report);
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