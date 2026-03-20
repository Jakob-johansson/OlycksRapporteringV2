using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class EditReportPage : ContentPage
{
    public EditReportPage(Report report)
    {
        InitializeComponent();
        BindingContext = new EditReportPageViewModel(report);
    }

    private async void OnBackClicked(object sender, EventArgs e) =>
        await Navigation.PopAsync();

    private void OnEditReportTitleClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("ReportTitle");

    private void OnEditReportDescriptionClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("ReportDescription");

    private void OnEditLocationClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("Location");

    private void OnEditTimeClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("Time");

    private void OnEditPeopleClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("People");

    private void OnEditMaterialClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("Material");

    private void OnEditImmediateClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("Immediate");

    private void OnEditPreventiveClicked(object sender, TappedEventArgs e) =>
        (BindingContext as EditReportPageViewModel)?.ToggleEdit("Preventive");

    private async void OnClickedGoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
