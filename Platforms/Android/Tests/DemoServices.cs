using Plugin.LocalNotification;
using WayofLife.Databases;
using WayofLife.Models;
using WayofLife.Platforms.Android;

namespace WayofLife.Android
{
    public class DemoServices
    {
        private readonly static ExpiryDatabase eDatabase = new();
        public static List<Expiry> eCollection = [];

        public static MainActivity MainActivity { get; set; }
        readonly bool stopping = false;
        public DemoServices()
        {

        }

        public async Task Run(CancellationToken token)
        {
            await Task.Run(async () =>
            {
                while (!stopping)
                {
                    token.ThrowIfCancellationRequested();
                    try
                    {
                        await Task.Delay(2000);

                        if (CheckExpiry() != "")
                        {
                            //https://www.youtube.com/watch?v=c_nbI0-FeOo
                            var request = new NotificationRequest
                            {
                                NotificationId = 1000,
                                Title = "Expired Item",
                                Description = "The following item(s) has expired: " + CheckExpiry(),
                                BadgeNumber = 42,
                                Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(0.3), NotifyRepeatInterval = TimeSpan.FromDays(1), }
                            };
                            await LocalNotificationCenter.Current.Show(request);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return;
            }, token);
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

        public async Task RefreshDataAsync()
        {
            try
            {

                // Fetch the data
                eCollection = await eDatabase.GetExpiriesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
