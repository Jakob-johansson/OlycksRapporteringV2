using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(string portal)
    {
        InitializeComponent();
        BindingContext = new LoginViewModel(portal);
    }

    private async Task OnClickedGoToAdminLoginPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginAdminPage());
    }
}