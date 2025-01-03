using Microsoft.Extensions.Logging;
using WayofLife.Pages;
using WayofLife.ViewModel;

namespace WayofLifev2
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
                        fonts.AddFont("SegoeFluentIcons.ttf", "FluentIcons");
                });

            builder.Services.AddSingleton<WayofLife.JournalDatabase>();

            builder.Services.AddTransient<AddJournal>();
            builder.Services.AddTransient<JournalViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
