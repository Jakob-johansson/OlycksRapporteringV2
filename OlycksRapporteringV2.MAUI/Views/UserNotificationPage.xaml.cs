using OlycksRapporteringV2.Domain.Entities;
using OlycksRapporteringV2.MAUI.ViewModels;

namespace OlycksRapporteringV2.MAUI.Views;

public partial class NotificationsPage : ContentPage
{
    public NotificationsPage()
    {
        InitializeComponent();
        BindingContext = new UserNotificationsPageViewModel();
    }

    //LADDA NOTISER VARJE GÅNG SIDAN VISAS\\
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as UserNotificationsPageViewModel).LoadNotifications();
    }

    private async void OnBackClicked(object sender, EventArgs e) =>
        await Navigation.PopAsync();

    //TRYCK PÅ EN NOTIS\\
    private async void OnNotificationTapped(object sender, TappedEventArgs e)
    {
        var vm = BindingContext as UserNotificationsPageViewModel;
        var border = sender as Border;
        var notification = border?.BindingContext as Notification;

        if (notification == null) return;

        //MARKERA SOM LÄST\\
        if (!notification.IsRead)
            await vm.MarkAsRead(notification);
    }
}
