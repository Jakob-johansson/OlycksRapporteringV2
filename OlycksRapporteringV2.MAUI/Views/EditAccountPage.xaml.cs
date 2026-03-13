using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class EditAccountPage : ContentPage
{
    public EditAccountPage()
    {
        InitializeComponent();
        BindingContext = new EditAccountViewModel();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnEditEmployeeIdClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("EmployeeId");
    }

    private void OnEditEmailClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("Email");
    }

    private void OnEditPhoneClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("Phone");
    }

    private void OnEditHomeAdressClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("HomeAdress");
    }

    private void OnEditPersonalBankClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("PersonalBank");
    }

    private void OnEditInsuranceClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("Insurance");
    }

    private void OnEditContactPerson1Clicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("ContactPerson1");
    }

    private void OnEditContactPerson1PhoneClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("ContactPerson1Phone");
    }

    private void OnEditContactPerson2Clicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("ContactPerson2");
    }

    private void OnEditContactPerson2PhoneClicked(object sender, TappedEventArgs e)
    {
        (BindingContext as EditAccountViewModel)?.ToggleEdit("ContactPerson2Phone");
    }
}
