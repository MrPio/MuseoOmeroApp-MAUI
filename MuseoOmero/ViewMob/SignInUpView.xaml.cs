using Mopups.Interfaces;
using MuseoOmero.ViewMob.Popups;

namespace MuseoOmero.ViewMob;

public partial class SignInUpView : ContentPage
{
	private readonly MainViewModel _mainViewModel;
	private readonly IPopupNavigation _popupNavigation;

	public SignInUpView(MainViewModel mainViewModel, IPopupNavigation popupNavigation)
	{
		_mainViewModel = mainViewModel;
		_popupNavigation = popupNavigation;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		new Task(CheckCached).RunSynchronously();
	}

	private async void CheckCached()
	{
		if (String.IsNullOrEmpty(await SecureStorage.Default.GetAsync("uid")))
			return;

		Loading.IsVisible = true;
		if (await AccountManager.Instance.CacheSignIn(false))
		{
			App.Current.MainPage = new MainView(_mainViewModel);
		}
		else
		{
			await DisplayAlert("Errore", "Le credenziali memorizzate non erano corrette, per favore, prova ad effettuare nuovamente l'accesso.", "Ok");
		}
		Loading.IsVisible = false;
	}

	private async void Accedi_Clicked(object sender, EventArgs e)
	{
		Loading.IsVisible = true;

		if (EmailEntry.Text.Length < 3 || PasswordEntry.Text.Length < 3)
		{
			await Application.Current.MainPage.DisplayAlert("Campi non compilati", "Assicurati di aver compilato correttamente i campi.", "OK");
			Loading.IsVisible = false;
			return;
		}

		try
		{
			var credential = await AccountManager.Instance.SignInCliente(EmailEntry.Text, PasswordEntry.Text);
		}
		catch (Exception ex)
		{
			if (ex.Message == "Account di cliente")
				await App.Current.MainPage.DisplayAlert("Attenzione", "Non puoi accedere al portale gestionale utilizzando le credenziali di un cliente! Per favore inserisci delle credenziali valide.", "Ok");
			else if (ex.Message == "Account rimosso")
				await App.Current.MainPage.DisplayAlert("Errore", "L'account è stato rimosso dal database, quindi non è possibile accedere. Per favore contatta l'assistenza per la creazione di un nuovo account.", "Ok");
			else
				await Application.Current.MainPage.DisplayAlert("Autenticazione non riuscita", "Email e/o Password errati. Per favore, assicurati di aver compilato correttamente i campi.", "OK");
			Loading.IsVisible = false;
			return;
		}
		finally
		{
			Loading.IsVisible = false;
		}
		App.Current.MainPage = new MainView(_mainViewModel);
	}
	private void Registrati_Clicked(object sender, EventArgs e)
	{
		var popup = new SignUpView(_mainViewModel,_popupNavigation);
		popup.Email = EmailEntry.Text;	
		popup.Password = PasswordEntry.Text;
		//App.Current.MainPage = popup;
		_popupNavigation.PushAsync(popup);
	}

	private void HighlightView_Pressed(object sender, EventArgs e)
	{
		PwdDimenticataLabel.FontAttributes = FontAttributes.Bold;
		PwdDimenticataLabel.TextDecorations = TextDecorations.Underline;
	}

	private void HighlightView_Released(object sender, EventArgs e)
	{
		PwdDimenticataLabel.CancelAnimation();
		PwdDimenticataLabel.FontAttributes = FontAttributes.None;
		PwdDimenticataLabel.TextDecorations = TextDecorations.None;
	}

	private async void HighlightView_Clicked(object sender, EventArgs e)
	{
		await AccountManager.Instance.ResetPassword();
	}
}