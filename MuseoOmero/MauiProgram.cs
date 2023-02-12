using CommunityToolkit.Maui;
using Microsoft.Maui.LifecycleEvents;
using Sharpnado.Tabs;
using Syncfusion.Maui.Core.Hosting;

namespace MuseoOmero;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Lato-Regular.ttf", "Lato");
                fonts.AddFont("Lato-Italic.ttf", "LatoItalic");
                fonts.AddFont("Lato-Light.ttf", "LatoLight");
                fonts.AddFont("Lato-LightItalic.ttf", "LatoLightItalic");
                fonts.AddFont("Lato-Bold.ttf", "LatoBold");
                fonts.AddFont("Lato-Black.ttf", "LatoBlack");
                fonts.AddFont(filename: "materialdesignicons-webfont.ttf", alias: "MaterialDesignIcons");
            })
            .UseSharpnadoTabs(loggerEnable: false)
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

                static void MakeStatusBarTranslucent(Android.App.Activity activity)
                {
                    //activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);
                    //activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                    activity.Window.SetStatusBarColor(Android.Graphics.Color.Rgb(8, 112, 59));
                    activity.Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(8, 112, 59));
                }
#endif
            });


        return builder.Build();
    }

}
