﻿#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Mopups.Interfaces;
using MuseoOmero.Messages;
using MuseoOmero.View;
using MuseoOmero.ViewWin;

namespace MuseoOmero;

public partial class App : Application
{

#if WINDOWS || MACCATALYST
	public App(SignInUpViewModelWin signInUpViewModelWin, ShellViewModelWin shellViewModelWin)
	{
		InitializeComponent();
		var width = 1360;
		var height = 920;

		Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
		{
			var mauiWindow = handler.VirtualView;
			var nativeWindow = handler.PlatformView;
			nativeWindow.Activate();
			IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
			WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
			AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
			var display = DeviceDisplay.Current.MainDisplayInfo;
			appWindow.MoveAndResize(new RectInt32((int)(display.Width / 2 - width / 2), (int)(display.Height / 2 - height / 2), width, height));
			appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
		});

		MainPage = new SignInUpViewWin(signInUpViewModelWin, shellViewModelWin);
		//MainPage=new BlankView();
		//load(shellViewModelWin);

		LiveCharts.Configure(config =>
				config
					.AddSkiaSharp()
					.AddDefaultMappers()
					.AddLightTheme()
				);

		WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (r, m) =>
		{
			LoadTheme(m.Value);
		});
		LoadTheme(Preferences.Get("AppTheme", "black"));
	}

	private void LoadTheme(string theme)
	{
		if (!MainThread.IsMainThread)
		{
			MainThread.BeginInvokeOnMainThread(() => LoadTheme(theme));
			return;
		}

		ResourceDictionary dictionary = theme switch
		{
			"green" => new Resources.Styles.GreenTheme(),
			"darkGreen" => new Resources.Styles.DarkGreenTheme(),
			"black" => new Resources.Styles.BlackTheme(),
			"highContrast" => new Resources.Styles.HighContrastTheme(),
			_ => null
		};
		var themes = new[] { "GreenTheme", "DarkGreenTheme", "BlackTheme", "HighContrastTheme" };
		foreach (var res in Resources.MergedDictionaries)
			if (res.Source is { } && themes.Any(t => res.Source.ToString().Contains(t)))
				Resources.MergedDictionaries.Remove(res);

		if (dictionary != null)
			Resources.MergedDictionaries.Add(dictionary);
	}

#elif ANDROID || IOS
public App(MainViewModel mainViewModel, IPopupNavigation popupNavigation)
	{
		InitializeComponent();

				MainPage = new SignInUpView(mainViewModel, popupNavigation);

		LiveCharts.Configure(config =>
				config
					.AddSkiaSharp()
					.AddDefaultMappers()
					.AddLightTheme()
				);
	}
#endif

	//async void load(ShellViewModelWin shellViewModelWin)
	//{
	//	if (await AccountManager.Instance.CacheSignIn())
	//		MainPage = new ShellViewWin(shellViewModelWin);
	//}
}
