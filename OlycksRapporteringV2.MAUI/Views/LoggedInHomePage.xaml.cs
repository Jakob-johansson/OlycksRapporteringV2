using OlycksRapporteringV2.Application.Services;
using MauiApp = Microsoft.Maui.Controls.Application;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class LoggedInHomePage : ContentPage
{
    public LoggedInHomePage()
    {
        InitializeComponent();
    }
    private async void OnClickedGoToCreateReportPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateReportPage());
    }

    private async void OnClickedGoToManageReportPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShowMyReportsPage());
    }

    private async void OnClickedGoToNotificationsPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotificationsPage());
    }

    private async void OnClickedGoToShowAccountPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShowAccountPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Avsluta session
        UserSession.EndSession();

        // Gå tillbaka till login-sidan
        await Navigation.PushAsync(new MainPage());
    }
}