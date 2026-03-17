using OlycksRapporteringV2.Application.Services;
using OlycksRapporteringV2.MAUI.ViewModels;
using MauiApp = Microsoft.Maui.Controls.Application;
namespace OlycksRapporteringV2.MAUI.Views;

public partial class ShowAccountPage : ContentPage
{
    public ShowAccountPage()
    {
        InitializeComponent();
        BindingContext = new ShowAccountViewModel();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logga ut", "Är du säker på att du vill logga ut?", "Ja", "Avbryt");
        if (confirm)
        {
            UserSession.Instance.EndSession();
            MauiApp.Current.MainPage = new NavigationPage(new MainPage());
        }
    }

    private void OnEditEmployeeIdClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("EmployeeId");

    private void OnEditEmailClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("Email");

    private void OnEditPhoneClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("Phone");

    private void OnEditHomeAdressClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("HomeAdress");

    private void OnEditPersonalBankClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("PersonalBank");

    private void OnEditInsuranceClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("Insurance");

    private void OnEditContactPerson1Clicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("ContactPerson1");

    private void OnEditContactPerson1PhoneClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("ContactPerson1Phone");

    private void OnEditContactPerson2Clicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("ContactPerson2");

    private void OnEditContactPerson2PhoneClicked(object sender, TappedEventArgs e) =>
        (BindingContext as ShowAccountViewModel)?.ToggleEdit("ContactPerson2Phone");
}