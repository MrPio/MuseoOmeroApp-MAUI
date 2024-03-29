﻿using BarcodeScanner.Mobile;
using CommunityToolkit.Maui;
using MauiSampleCamera;
using Microsoft.Maui.LifecycleEvents;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
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
			.UseSkiaSharp(true)
			.UseBarcodeReader()
			.ConfigureMopups()
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
					activity.Window.SetStatusBarColor(Android.Graphics.Color.Rgb(33, 33, 33));
					activity.Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(33, 33, 33));
				}
#endif
			});

		// WINDOWS
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
		builder.Services.AddSingleton<AccountViewWin>();
		builder.Services.AddSingleton<AccountViewModelWin>();
		builder.Services.AddSingleton<StatisticheViewWin>();
		builder.Services.AddSingleton<StatisticheViewModelWin>();
		builder.Services.AddSingleton<IMediaPicker, CustomMediaPicker>();
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

		// ANDROID
#if ANDROID 
		builder.Services.AddSingleton<SignInUpView>();
		builder.Services.AddSingleton<MainView>();
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<IMieiTitoliView>();
		builder.Services.AddSingleton<IMieiTitoliViewModel>();
		builder.Services.AddSingleton<BiglietteriaView>();
		builder.Services.AddSingleton<BiglietteriaViewModel>();
		builder.Services.AddSingleton<HomeViewModel>();
		builder.Services.AddSingleton<ChatViewModel>();
		builder.Services.AddSingleton<StatisticheViewModel>();
		builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
#endif


		return builder.Build();
	}

}
