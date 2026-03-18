using OlycksRapporteringV2.MAUI.Models;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class AdminArchivePage : ContentPage
{
    public AdminArchivePage()
    {
        InitializeComponent();
        BindingContext = new AdminArchivePageViewModel();
    }

    //LADDA RAPPORTER VARJE GÅNG SIDAN VISAS\\
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as AdminArchivePageViewModel).LoadReports();
    }

    private async void OnBackClicked(object sender, EventArgs e) =>
        await Navigation.PopAsync();

    private void OnSelectModeClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as AdminArchivePageViewModel;
        vm.SelectModeActive ^= true;
        if (!vm.SelectModeActive)
            vm.ClearSelections();
    }

    private async void OnDeleteSelectedClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as AdminArchivePageViewModel;
        var valda = vm.Reports.Where(r => r.IsSelected).ToList();

        if (!valda.Any())
        {
            await DisplayAlertAsync("Ingen vald", "Välj minst en rapport.", "OK");
            return;
        }

        bool confirm = await DisplayAlertAsync(
            "Radera",
            $"Är du säker på att du vill radera {valda.Count} rapport(er) permanent?",
            "Ja", "Avbryt");

        if (confirm)
            await vm.DeleteSelected();
    }

    private async void OnReportTapped(object sender, TappedEventArgs e)
    {
        var vm = BindingContext as AdminArchivePageViewModel;
        var border = sender as Border;
        var item = border?.BindingContext as SelectableReport;

        if (item == null) return;

        if (vm.SelectModeActive)
            vm.ToggleSelection(item);
        else
            await Navigation.PushAsync(new AdminReportDetailPage(item.Report));
    }
}
