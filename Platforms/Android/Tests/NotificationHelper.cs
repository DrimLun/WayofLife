using Android.App;
using Android.Content;
using AndroidX.Core.App;

namespace WayofLife.Platforms.Android
{
    internal class NotificationHelper
    {
        //channelID is the ID of the channel that the notification will be sent to, must be unique
        private string foregroundChannelId = "9001";

        private readonly Context context = global::Android.App.Application.Context;

        public Notification GetServiceStartedNotification()
        { 
            string expiredItems = "";

            var notificationBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
                .SetContentTitle("Expired Items")
                .SetContentText(expiredItems)
                .SetOngoing(true);

            if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
            {
                var notificationChannel = new NotificationChannel(foregroundChannelId, "Way of Life", NotificationImportance.High);
                var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                notificationManager.CreateNotificationChannel(notificationChannel);
                
            }

            return notificationBuilder.Build();
        }
    }
}
