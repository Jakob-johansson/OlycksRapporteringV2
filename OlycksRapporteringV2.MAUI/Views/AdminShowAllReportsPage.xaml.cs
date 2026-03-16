using OlycksRapporteringV2.MAUI.Models;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class AdminShowAllReportsPage : ContentPage
{
    public AdminShowAllReportsPage()
    {
        InitializeComponent();

        BindingContext = new AdminShowAllReportsPageViewModel();
    }



    //OnAppearing ANVÄNDS FÖR ATT LADDA RAPPORTEN VARJE GÅNG SIDAN VISAS\\
    //INTE BARA FÖRSTA GÅNGEN\\ 
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as AdminShowAllReportsPageViewModel).LoadReports();
    }
    private void OnFilterCreatedClicked(object sender, EventArgs e) =>
    (BindingContext as AdminShowAllReportsPageViewModel).FilterByStatus("Created");

    private void OnFilterPendingClicked(object sender, EventArgs e) =>
        (BindingContext as AdminShowAllReportsPageViewModel).FilterByStatus("Pending");

    private void OnFilterUnderReviewClicked(object sender, EventArgs e) =>
        (BindingContext as AdminShowAllReportsPageViewModel).FilterByStatus("UnderReview");

    private void OnFilterApprovedClicked(object sender, EventArgs e) =>
        (BindingContext as AdminShowAllReportsPageViewModel).FilterByStatus("Approved");

    private void OnFilterRejectedClicked(object sender, EventArgs e) =>
        (BindingContext as AdminShowAllReportsPageViewModel).FilterByStatus("Rejected");
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnSelectModeClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Status", "Sätt till: Created", "OK");
    }

    private async void OnDeleteSelectedClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Status", "Sätt till: Created", "OK");
    }

    private async void OnReportTapped(object sender, TappedEventArgs e)
    {
        var vm = BindingContext as AdminShowAllReportsPageViewModel;
        var border = sender as Border;
        var item = border?.BindingContext as SelectableReport;
        if (item == null) return;

        if (vm.SelectModeActive)
            vm.ToggleSelection(item);
        else
             await Navigation.PushAsync(new AdminReportDetailPage(item.Report));
    }

    private void OnFilterAllClicked(object sender, EventArgs e)
    {
        (BindingContext as AdminShowAllReportsPageViewModel).FilterByStatus(null);
    }
}
