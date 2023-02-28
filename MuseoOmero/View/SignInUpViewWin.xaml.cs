namespace MuseoOmero.ViewWin;

public partial class SignInUpViewWin : ContentPage
{
	private readonly SignInUpViewModelWin _viewModel;
	private readonly ShellViewModelWin _shellViewModelWin;
	public SignInUpViewWin(SignInUpViewModelWin viewModel, ShellViewModelWin shellViewModelWin)
	{
		_viewModel = viewModel;
		_shellViewModelWin = shellViewModelWin;
		BindingContext = _viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		new Task(CheckCached).RunSynchronously();
	}

	private async void CheckCached()
	{
		await Task.Delay(2000);

		//await DbPopulatorManager.Instance.populateUtenti();
		//await DbPopulatorManager.Instance.populateChats();
		//await DbPopulatorManager.Instance.populateDipendenti();
		if (String.IsNullOrEmpty(await SecureStorage.Default.GetAsync("uid")))
			return;

		Loading.IsVisible = true;
		if (await AccountManager.Instance.CacheSignIn())
		{
			App.Current.MainPage = new ShellViewWin(_shellViewModelWin);
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
			var credential = await AccountManager.Instance.SignIn(EmailEntry.Text, PasswordEntry.Text);
		}
		catch (Exception ex)
		{
			if (ex.Message == "Account di cliente")
				await App.Current.MainPage.DisplayAlert("Attenzione", "Non puoi accedere al portale gestionale utilizzando le credenziali di un cliente! Per favore inserisci delle credenziali valide.", "Ok");
			else if (ex.Message == "Account rimosso")
				await App.Current.MainPage.DisplayAlert("Errore", "L'account è stato rimosso dal database, quindi non è possibile accedere. Per favore contatta l'assistenza per la creazione di un nuovo account.", "Ok");
			else
				await Application.Current.MainPage.DisplayAlert("Autenticazione non riuscita", "Assicurati di aver compilato correttamente i campi.", "OK");
			Loading.IsVisible = false;
			return;
		}
		finally
		{
			Loading.IsVisible = false;
		}
		App.Current.MainPage = new ShellViewWin(_shellViewModelWin);
	}

	private void HighlightView_Pressed(object sender, EventArgs e)
	{
		PwdDimenticataLabel.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[2], c => PwdDimenticataLabel.TextColor = c, 350, Easing.CubicOut);
		PwdDimenticataLabel.FontAttributes = FontAttributes.Bold;
		PwdDimenticataLabel.TextDecorations = TextDecorations.Underline;
	}

	private void HighlightView_Released(object sender, EventArgs e)
	{
		PwdDimenticataLabel.CancelAnimation();
		PwdDimenticataLabel.ColorTo(DeviceManager.Instance.Colors[1], DeviceManager.Instance.Colors[0], c => PwdDimenticataLabel.TextColor = c, 350, Easing.CubicOut);
		PwdDimenticataLabel.FontAttributes = FontAttributes.None;
		PwdDimenticataLabel.TextDecorations = TextDecorations.None;
	}

	private async void HighlightView_Clicked(object sender, EventArgs e)
	{
		var email = await DisplayPromptAsync("Email", "Inserisci l'email dell'account dove invieremo la procedura necessaria al ripristino della password.", placeholder: "email@example.com", maxLength: 50);

		if (!String.IsNullOrEmpty(email))
		{
			if (email.Contains('@') && email.Contains('.'))
			{
				await AccountManager.Instance.ResetPassword(email);
				await DisplayAlert("Successo", "Per favore, controlla la tua casella di posta per reimpostare la password.", "Ok");

			}
			else
			{
				await DisplayAlert("Attenzione", "Per favore, inserisci un'email valida.", "Riprova");
				HighlightView_Clicked(null, null);
			}
		}
	}
}