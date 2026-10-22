using LessonOne.models;
using LessonOne.services;
using Microsoft.Extensions.Logging;

namespace LessonOne
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            DBService.Init();
            DBService.Connection.Insert(new Projects { Name = "abc", experements = "abc", reports = "abc" });

            return builder.Build();
        }
    }
}
