using BarcodeScanner.Mobile;
using CommunityToolkit.Maui;
using MauiSampleCamera;
using Microsoft.Maui.LifecycleEvents;
using Sharpnado.Tabs;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Text;
using ZXing.Net.Maui.Controls;

namespace MuseoOmero;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Lato-Regular.ttf", "Lato");
				fonts.AddFont("Lato-Italic.ttf", "LatoItalic");
				fonts.AddFont("Lato-Light.ttf", "LatoLight");
				fonts.AddFont("Lato-LightItalic.ttf", "LatoLightItalic");
				fonts.AddFont("Lato-Bold.ttf", "LatoBold");
				fonts.AddFont("Lato-Black.ttf", "LatoBlack");
				fonts.AddFont(filename: "materialdesignicons-webfont.ttf", alias: "MaterialDesignIcons");
				fonts.AddFont(filename: "materialdesignicons-webfont_light.ttf", alias: "MaterialDesignIconsLight");
				fonts.AddFont(filename: "materialdesignicons-webfont_thin.ttf", alias: "MaterialDesignIconsThin");
			})
			.UseSharpnadoTabs(loggerEnable: false)
			.UseSkiaSharp().UseBarcodeReader()
			.ConfigureMauiHandlers(handlers =>
			{
				// Add the handlers
				handlers.AddBarcodeScannerHandler();
			})
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


#if WINDOWS
		builder.Services.AddTransient<SignInUpViewModelWin>();
		builder.Services.AddSingleton<ShellViewModelWin>();
        builder.Services.AddSingleton<HomeViewWin>();
		builder.Services.AddSingleton<HomeViewModelWin>();
		builder.Services.AddSingleton<OpereViewModelWin>();
		builder.Services.AddSingleton<OpereViewWin>();
		builder.Services.AddSingleton<BiglietteriaViewWin>();
		builder.Services.AddSingleton<BiglietteriaViewModelWin>();
		builder.Services.AddSingleton<ChatViewWin>();
		builder.Services.AddSingleton<ChatViewModelWin>();
#endif
		builder.Services.AddSingleton<IMediaPicker, CustomMediaPicker>();
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

		return builder.Build();
	}

}
