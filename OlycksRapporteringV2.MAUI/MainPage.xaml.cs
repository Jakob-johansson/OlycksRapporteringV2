using OlycksRapporteringV2.MAUI.Views;

namespace OlycksRapporteringV2.MAUI
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            //CreateAdminUser();

        }

        private async void OnClickedGoToPortalPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PortalPage());
        }

        public async void OnClickedGoToCreateAccountPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccount());
        }
    }
}
