namespace MuseoOmero.View.TemplatesWin;

public partial class TopMenu : ContentView
{
	public static List<TopMenu> TopMenus = new();
	public static string Url = null;
	public static bool UrlSet;
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
}