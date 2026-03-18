using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.MAUI.ViewModels;
using MauiApp = Microsoft.Maui.Controls.Application;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class LoggedInAdminPage : ContentPage
{
    public LoggedInAdminPage()
    {
        InitializeComponent();
        BindingContext = new LoggedInAdminPageViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as LoggedInAdminPageViewModel).LoadStats();
    }

    //LOGGA UT\\
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logga ut", "Är du säker?", "Ja", "Avbryt");
        if (confirm)
        {
            UserSession.Instance.EndSession();
            MauiApp.Current.MainPage = new NavigationPage(new MainPage());
        }
    }

    //RAPPORTHANTERING\\
    private async void OnApprovedReportsClicked(object sender, EventArgs e)
    {
        // kommer när vi bygger godkända-sidan
        await DisplayAlert("Kommer snart", "Godkända rapporter — under utveckling.", "OK");
    }

    private async void OnAllReportsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminShowAllReportsPage());
    }

    private async void OnArchivedReportsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Arkiv — under utveckling.", "OK");
    }

    private async void OnMessagesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminNotificationsPage());
    }
}