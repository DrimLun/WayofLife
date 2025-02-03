using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using WayofLife.Databases;
using WayofLife.Android;
using WayofLife.Pages;

namespace WayofLife
{
    public static class MauiProgram
    {

        private readonly static ExpiryDatabase eDatabase = new();

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            RFExpiry();
            if (ExpiryPage.CheckExpiry() != "")
            {
                //https://www.youtube.com/watch?v=c_nbI0-FeOo
                var request = new NotificationRequest
                {
                    NotificationId = 1000,
                    Title = "Expired Item",
                    Description = "The following item(s) has expired: " + ExpiryPage.CheckExpiry(),
                    BadgeNumber = 42,
                    Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(0.3), NotifyRepeatInterval = TimeSpan.FromDays(1), }
                };
                LocalNotificationCenter.Current.Show(request);
            }

        #if ANDROID
            builder.Services.AddTransient<IServiceTest, DemoServices>();
        #endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private async static Task RFExpiry()
        {
            ExpiryPage.eCollection = await eDatabase.GetExpiriesAsync();
        }
            
    }
}
