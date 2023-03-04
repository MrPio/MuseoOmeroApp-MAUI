using System.ComponentModel;

namespace MuseoOmero.View.TemplatesWin;

public partial class TopMenu : ContentView
{
	public static List<TopMenu> TopMenus = new();
	public static string Url = null;
	public static bool UrlSet;

	bool fullscreenState = true;
	public bool FullscreenState
	{
		get { return fullscreenState; }
		set { fullscreenState = value; OnPropertyChanged(); }
	}

	public string Nome => AccountManager.Instance.Dipendente.Nome;
	public string Cognome => AccountManager.Instance.Dipendente.Cognome;

	public bool Update = false;

	public TopMenu()
	{
		InitializeComponent();
		Initialize();
		TopMenus.Add(this);
	}


	public async void Initialize()
	{
		LoadingView.IsVisible = true;
		if (!UrlSet)
			Url = await StorageManager.Instance.GetLink($"{AccountManager.Instance.Uid}/foto_profilo/");
		UrlSet = true;
		Avatar.Source = Url is null ? ImagesOnline.Anonymous : Url;
		LoadingView.IsVisible = false;
	}

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		var shellViewModel = this.Handler.MauiContext.Services.GetService<ShellViewModelWin>();
		shellViewModel.SelectedRoute = "account";
	}

	private async void Logout_Tapped(object sender, EventArgs e)
	{
		if (await App.Current.MainPage.DisplayAlert("Disconnessione", "Sei sicuro di voler uscire dall'account?", "Si", "No"))
		{
			AccountManager.Instance.FirebaseAuthClient.SignOut();
			AccountManager.Instance.DeleteCache();

			App.Current.MainPage = new SignInUpViewWin(Handler.MauiContext.Services.GetService<SignInUpViewModelWin>(), Handler.MauiContext.Services.GetService<ShellViewModelWin>());
		}
	}

	private void Refresh_Clicked(object sender, EventArgs e)
	{
		var _shellViewModelWin = App.Current.MainPage.Handler.MauiContext.Services.GetService<ShellViewModelWin>();
		var tmp = _shellViewModelWin.SelectedRoute;
		_shellViewModelWin.SelectedRoute = "blank";
		_shellViewModelWin.SelectedRoute = tmp;
	}

	private void Fullscreen_Clicked(object sender, EventArgs e)
	{
		if (MainGrid.AnimationIsRunning("FullscreenAnimation"))
			return;
		Func<double, double> fun = FullscreenState ? v => 120 - v * 60 : v => 60 + v * 60;
		MainGrid.Animate(
			name: "FullscreenAnimation",
			animation: new(v => { 
				MainGrid.HeightRequest = fun(v);
				((Grid)Parent).RowDefinitions[0] = new(fun(v));
			}, easing: Easing.CubicOut),
			length: 400
		);
		FullscreenState = !FullscreenState;

	}
}