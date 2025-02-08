using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Plugin.LocalNotification;
using WayofLife.Databases;
using WayofLife.Models;

namespace WayofLife.Platforms.Android
{
    [Service]
    public class ForegroundServiceDemo : Service
    {
        private string NOTIFICATION_CHANNEL_ID = "2001";
        private int NOTIFICATION_ID = 690;
        private string NOTIFICATION_CHANNEL_NAME = "notification";

        private readonly static ExpiryDatabase eDatabase = new();
        private static List<Expiry> eCollection = [];
        private static Random random = new ();
        private string selectedQuote = MainPage.selectedQuotes[random.Next(0, MainPage.selectedQuotes.Count - 1)];
        private async void StartForegroundService()
        {
            var notifcationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                CreateNotificationChannel(notifcationManager);
            }

            var notification = new Notification.Builder(this, NOTIFICATION_CHANNEL_ID);
            notification.SetAutoCancel(true);
            notification.SetOngoing(true);

            await RFExpiry();
            notification.SetContentTitle("Motivational Quote");
            notification.SetContentText(selectedQuote);
            StartForeground(NOTIFICATION_ID, notification.Build());
        }

        private void CreateNotificationChannel(NotificationManager notificationMnaManager)
        {
            var channel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, NOTIFICATION_CHANNEL_NAME,
            NotificationImportance.High);
            notificationMnaManager.CreateNotificationChannel(channel);
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            StartForegroundService();
            return StartCommandResult.NotSticky;
        }

        private static async Task RFExpiry()
        {
            eCollection = await eDatabase.GetExpiriesAsync();
        }

        public static string CheckExpiry()
        {

            string expiredItemList = "";
            foreach (var item in eCollection)
            {
                if (item.ExpiryDateTime < DateTime.Now)
                {
                    expiredItemList += "\n· " + item.Name; //interpunct
                }
            }

            return expiredItemList;
        }
    }
}
