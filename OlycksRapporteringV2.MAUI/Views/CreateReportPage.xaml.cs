namespace OlycksRapporteringV2.MAUI.Views;

public partial class CreateReportPage : ContentPage
{
    public CreateReportPage()
    {

        InitializeComponent();
        BindingContext = new CreateReportViewModel();
    }
}