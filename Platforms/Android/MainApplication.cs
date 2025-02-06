using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace WayofLife.Platforms.Android;

[Application]
public class MainApplication : MauiApplication
{
    public static readonly string ChannelId = "backgroundServiceChannel";

    public MainApplication(nint handle, JniHandleOwnership ownership) : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() =>
        MauiProgram.CreateMauiApp();

    public override void OnCreate()
    {
        base.OnCreate();

#if ANDROID
        var context = Context; // Fix for CS1061 and CS8602
        Intent intent = new Intent(context, typeof(ForegroundServiceDemo)); // Fix for CS0246
        context.StartForegroundService(intent);
#endif

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
#pragma warning disable CA1416
            var serviceChannel = new NotificationChannel(ChannelId, "Background Service Channel", NotificationImportance.High);

            if (GetSystemService(NotificationService) is NotificationManager manager)
            {
                manager.CreateNotificationChannel(serviceChannel);
            }
#pragma warning restore CA1416
        }
    }
}
