using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.MAUI.Models;
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
        }
        else
        {
            //HÄR FÅR ADMIN NOTIS FRÅN ANVÄNDARE OM ATT DE VILL ÄNDRA RAPPORTEN\\
            await DisplayAlertAsync("Ej tillåtet", "Rapporten har redan ganskats. En förfrågan skickas till ansvarig", "OK");
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

        if (vm.SelectModeActive)
            vm.ToggleSelection(item);
        else
            vm.SelectedReport = item.Report;
    }
}