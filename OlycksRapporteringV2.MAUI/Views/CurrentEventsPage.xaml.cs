using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class CurrentEventsPage : ContentPage
{
    public CurrentEventsPage()
    {
        InitializeComponent();
        BindingContext = new CurrentEventsPageViewModel();
    }

    //LADDA HÄNDELSER VARJE GÅNG SIDAN VISAS\\
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as CurrentEventsPageViewModel).LoadIncidents();
    }

    private async void OnBackClicked(object sender, EventArgs e) =>
        await Navigation.PopAsync();
}
