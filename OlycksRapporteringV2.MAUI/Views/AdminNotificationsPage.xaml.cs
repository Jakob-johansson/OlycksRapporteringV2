using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class AdminNotificationsPage : ContentPage
{
    public AdminNotificationsPage()
    {
        InitializeComponent();
        BindingContext = new AdminNotificationsPageViewModel();
    }

    //LADDA NOTISER VARJE GÅNG SIDAN VISAS\\
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as AdminNotificationsPageViewModel).LoadNotifications();
    }

    private async void OnBackClicked(object sender, EventArgs e) =>
        await Navigation.PopAsync();

    //TRYCK PÅ EN NOTIS\\
    private async void OnNotificationTapped(object sender, TappedEventArgs e)
    {
        var vm = BindingContext as AdminNotificationsPageViewModel;
        var border = sender as Border;
        var notification = border?.BindingContext as Notification;

        if (notification == null) return;

        //MARKERA SOM LÄST\\
        if (!notification.IsRead)
            await vm.MarkAsRead(notification);

        //HÄMTA RAPPORTEN OCH NAVIGERA TILL DETAILSIDA\\
        var report = await vm.GetReportForNotification(notification);
        if (report != null)
            await Navigation.PushAsync(new AdminReportDetailPage(report));
        else
            await DisplayAlert("Fel", "Rapporten hittades inte.", "OK");
    }
}
