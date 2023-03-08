using Mopups.Interfaces;
using System.Runtime.CompilerServices;

namespace MuseoOmero.ViewMob.Popups;

public partial class SignUpView: Mopups.Pages.PopupPage
{
	private readonly MainViewModel _mainViewModel;
	private readonly IPopupNavigation _popupNavigation;


	private string email = "", password = "", confermaPassword = "", username = "", nome = "",
		cognome = "", cellulare = "";
	public string Email
	{
		get => email; set => email = value;
	}
	public string Username
	{
		get => username; set { username = value; }
	}
	public string Password
	{
		get => password; set { password = value; }
	}
	public string ConfermaPassword
	{
		get => confermaPassword; set { confermaPassword = value; }
	}
	public string Nome
	{
		get => nome; set { nome = value; }
	}
	public string Cognome
	{
		get => cognome; set { cognome = value; }
	}
	public string Cellulare
	{
		get => cellulare; set { cellulare = value; }
	}
	public SignUpView(MainViewModel mainViewModel, IPopupNavigation popupNavigation)
	{
		_mainViewModel = mainViewModel;
		_popupNavigation = popupNavigation;
		BindingContext = this;
		InitializeComponent();
	}

	private async void Conferma_Clicked(object sender, EventArgs e)
	{
		var am = AccountManager.Instance;
		var color = DeviceManager.Instance.Colors[0];
		if (App.Current.PlatformAppTheme == AppTheme.Dark)
			color = DeviceManager.Instance.Colors[4];

		EmailEntry.Color = color;
		UsernameEntry.Color = color;
		PasswordEntry.Color = color;
		ConfermaPasswordEntry.Color = color;
		var errore = false;

		if (Email.Length < 3 || Password.Length < 6)
		{
			DisplayAlert("Errore", "Per favore, inserisci una email e una password validi", "Ok");
			EmailEntry.Color = Colors.DarkOrange;
			PasswordEntry.Color = Colors.DarkOrange;
			EmailEntry.UnfocusedColor = Colors.DarkOrange;
			PasswordEntry.UnfocusedColor = Colors.DarkOrange;

			errore = true;
		}
		if (Username.Length < 3)
		{
			DisplayAlert("Errore", "Per favore, inserisci un username valido", "Ok");
			UsernameEntry.UnfocusedColor = Colors.DarkOrange;
			UsernameEntry.Color = Colors.DarkOrange;
			errore = true;
		}

		if (Password != ConfermaPassword)
		{

			DisplayAlert("Errore", "Per favore, assicurati che le password siano uguali", "Ok");
			PasswordEntry.Color = Colors.DarkOrange;
			ConfermaPasswordEntry.Color = Colors.DarkOrange;
			PasswordEntry.UnfocusedColor = Colors.DarkOrange;
			ConfermaPasswordEntry.UnfocusedColor = Colors.DarkOrange;
			errore = true;
		}
		if (errore)
			return;
		Loading.IsVisible = true;
		try
		{
			await am.SignUpCliente(EmailEntry.Text, PasswordEntry.Text, Username, Nome, Cognome, Cellulare);
		}
		catch (Exception ex)
		{
			DisplayAlert("Errore generico", "Spiacente, si è verificato un errore generico, si prega di riprovare", "Ok");
			return;
		}
		await _popupNavigation.PopAllAsync();
		App.Current.MainPage = new MainView(_mainViewModel);
		ErrorLabel.IsVisible = true;
		Loading.IsVisible = false;
	}

}