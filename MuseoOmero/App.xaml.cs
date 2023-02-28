#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using MuseoOmero.View;
using MuseoOmero.ViewWin;

namespace MuseoOmero;

public partial class App : Application
{
    public App(SignInUpViewModelWin signInUpViewModelWin, ShellViewModelWin shellViewModelWin)
    {
        InitializeComponent();

		Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
		{
#if WINDOWS
			var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            var display=DeviceDisplay.Current.MainDisplayInfo;
            appWindow.MoveAndResize(new RectInt32((int)(display.Width/2-1360/2),(int)(display.Height/2-850/2),1360,850));
            appWindow.TitleBar.ExtendsContentIntoTitleBar=true;
#endif
		});

		var loggedIn = true;
        if (loggedIn)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                MainPage = new MainView();
            else if (DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                //MainPage = new ShellViewWin(shellViewModelWin);
                MainPage = new SignInUpViewWin(signInUpViewModelWin, shellViewModelWin);
        }
        else
        {
            //MainPage = new LoginPage();
        }

		LiveCharts.Configure(config =>
				config
					// registers SkiaSharp as the library backend
					// REQUIRED unless you build your own
					.AddSkiaSharp()

					// adds the default supported types
					// OPTIONAL but highly recommend
					.AddDefaultMappers()

					// select a theme, default is Light
					// OPTIONAL
					//.AddDarkTheme()
					.AddLightTheme()

					// finally register your own mappers
					// you can learn more about mappers at:
					// ToDo add website link...
					//.HasMap<City>((city, point) =>
					//{
					//	point.PrimaryValue = city.Population;
					//	point.SecondaryValue = point.Context.Index;
					//})
				// .HasMap<Foo>( .... )
				// .HasMap<Bar>( .... )
				);
	}
}
