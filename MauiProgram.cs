using Microsoft.Extensions.Logging;
using WayofLife.Pages;
using WayofLifev2.ViewModel;

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


/* Unmerged change from project 'WayofLifev2 (net9.0-maccatalyst)'
Before:
            builder.Services.AddSingleton<WayofLife.JournalDatabase>();
After:
            builder.Services.AddSingleton<JournalDatabase>();
*/
            builder.Services.AddSingleton<WayofLifev2.Database_File.JournalDatabase>();

            builder.Services.AddTransient<AddJournal>();
            builder.Services.AddTransient<JournalViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
