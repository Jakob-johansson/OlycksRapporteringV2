namespace OlycksRapporteringV2.MAUI.Views;

public partial class PortalPage : ContentPage
{
    public PortalPage()
    {
        InitializeComponent();
    }

    private async void EmployeeLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage("EmployeePortal"));
    }

    private async void SafetyLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage("SafetyPortal"));
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}