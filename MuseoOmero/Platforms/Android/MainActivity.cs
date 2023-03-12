using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace MuseoOmero;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize |  ConfigChanges.UiMode  | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density| ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
public class MainActivity : MauiAppCompatActivity
{
	protected override void OnCreate(Bundle savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		Platform.Init(this, savedInstanceState);

		this.Window?.AddFlags(WindowManagerFlags.Fullscreen);
		this.Window?.AddFlags(WindowManagerFlags.BlurBehind);
		this.Window?.AddFlags(WindowManagerFlags.DimBehind);
		//this.Window?.AddFlags(WindowManagerFlags.TranslucentNavigation);
		this.Window?.AddFlags(WindowManagerFlags.TranslucentStatus);
		this.Window?.AddFlags(WindowManagerFlags.HardwareAccelerated);
		//this.Window?.AddFlags(WindowManagerFlags.ShowWhenLocked);

	}
}
