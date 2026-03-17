using OlycksRapporteringV2.Application.Services;
using MauiApp = Microsoft.Maui.Controls.Application;
namespace OlycksRapporteringV2.MAUI.Views;




public partial class LoggedInAdminPage : ContentPage
{
    public LoggedInAdminPage()
    {
        InitializeComponent();
        // ingen BindingContext behövs här
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logga ut", "Är du säker?", "Ja", "Avbryt");
        if (confirm)
        {
            UserSession.Instance.EndSession();
            MauiApp.Current.MainPage = new NavigationPage(new MainPage());
        }
    }

    private async void OnAllReportsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminShowAllReportsPage());
    }

    private async void OnArchivedReportsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Arkiv — under utveckling.", "OK");
    }

    private async void OnSetCreatedClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Status", "Sätt till: Created", "OK");
    }

    private async void OnSetPendingClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Status", "Sätt till: Pending", "OK");
    }

    private async void OnSetUnderReviewClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Status", "Sätt till: UnderReview", "OK");
    }

    private async void OnSetApprovedClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Status", "Sätt till: Approved", "OK");
    }

    private async void OnSetRejectedClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Status", "Sätt till: Rejected", "OK");
    }

    private async void OnArchiveClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Arkivera", "Arkivera rapport — under utveckling.", "OK");
    }

    private async void OnAllUsersClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Användarhantering — under utveckling.", "OK");
    }

    private async void OnManageStatusClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Användarhantering — under utveckling.", "OK");
    }

    private async void OnFilterCreatedClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Användarhantering — under utveckling.", "OK");
    }

    private async void OnFilterPendingClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Användarhantering — under utveckling.", "OK");
    }

    private async void OnFilterUnderReviewClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Användarhantering — under utveckling.", "OK");
    }

    private async void OnFilterApprovedClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Användarhantering — under utveckling.", "OK");
    }

    private async void OnFilterRejectedClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Kommer snart", "Användarhantering — under utveckling.", "OK");
    }

    private async void OnMessagesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminNotificationsPage());
    }

  }

