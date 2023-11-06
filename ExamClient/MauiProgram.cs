using Microsoft.Extensions.Logging;

namespace Client
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
                })
                //.ConfigureMultilanguage(config =>
                //{
                //    // Set the source of the translations
                //    // You can use multiple resource managers by calling UseResource multiple times.
                //    config.UseResource(AppTranslations.ResourceManager);

                //    // If the app is not storing last used culture, this culture will be used by default
                //    config.UseDefaultCulture(new System.Globalization.CultureInfo("en-US"));

                //    // Determines whether the app should store the last used culture
                //    config.StoreLastUsedCulture(true);

                //    // Determines whether the app should throw an exception if a translation is not found.
                //    config.ThrowExceptionIfTranslationNotFound(false);

                //    // You can set custom translation not found text by calling this method 
                //    config.SetTranslationNotFoundText("Transl_Not_Found:", appendTranslationKey: true);
                //})
                ;

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}