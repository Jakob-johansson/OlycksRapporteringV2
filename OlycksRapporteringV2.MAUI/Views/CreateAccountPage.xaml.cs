using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class CreateAccount : ContentPage
{
    public CreateAccount()
    {
        InitializeComponent();
        BindingContext = new CreateUserViewModel();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}