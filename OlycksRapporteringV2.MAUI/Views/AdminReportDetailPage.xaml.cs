using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.Domain.Enums;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class AdminReportDetailPage : ContentPage
{
    public AdminReportDetailPage(Report report)
    {
        InitializeComponent();
        BindingContext = new AdminReportDetailPageViewModel(report);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    //SÄTT STATUS\\
    private async void OnSetCreatedClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.Created);

    private async void OnSetPendingClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.Pending);

    private async void OnSetUnderReviewClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.UnderReview);

    private async void OnSetApprovedClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.Approved);

    private async void OnSetRejectedClicked(object sender, EventArgs e) =>
        await (BindingContext as AdminReportDetailPageViewModel).SetStatus(ReportStatus.Rejected);
}
