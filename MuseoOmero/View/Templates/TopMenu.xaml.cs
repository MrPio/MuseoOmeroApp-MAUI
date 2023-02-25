namespace MuseoOmero.View.TemplatesWin;

public partial class TopMenu : ContentView
{
	public string Nome => AccountManager.Instance.Account.Nome;
	public string Cognome => AccountManager.Instance.Account.Cognome;
	public TopMenu()
	{
		InitializeComponent();
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
			AccountManager.Instance.DeleteCache();

			App.Current.MainPage = new SignInUpViewWin(Handler.MauiContext.Services.GetService<SignInUpViewModelWin>(), Handler.MauiContext.Services.GetService<ShellViewModelWin>());
		}
	}
}