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
        //await Navigation.PushAsync(new ManageReportPage());
    }

    private async void OnClickedGoToNotificationsPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotificationsPage());
    }
}