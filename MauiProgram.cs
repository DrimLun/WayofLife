using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using WayofLife.Databases;
using WayofLife.Models;
using WayofLife.Pages;

namespace WayofLife
{
    public static class MauiProgram
    {


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

        #if ANDROID
            //builder.Services.AddTransient<IServiceTest, DemoServices>();
        #endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

    }
}
