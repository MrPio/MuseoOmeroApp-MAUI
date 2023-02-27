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
		//await DbPopulatorManager.Instance.populateUtenti();
		await DbPopulatorManager.Instance.populateChats();

		await Task.Delay(400); //TODO
		Loading.IsVisible = true;
		if (await AccountManager.Instance.CacheSignIn())
			App.Current.MainPage = new ShellViewWin(_shellViewModelWin);
		Loading.IsVisible = false;
	}
	private async void Accedi_Clicked(object sender, EventArgs e)
	{
		Loading.IsVisible = true;
		await DbPopulatorManager.Instance.populateUtenti();
		if (EmailEntry.Text.Length < 3 || PasswordEntry.Text.Length < 3)
		{
			await Application.Current.MainPage.DisplayAlert("Campi non compilati", "Assicurati di aver compilato correttamente i campi.", "OK");
			Loading.IsVisible = false;
			return;
		}
		var credential = await AccountManager.Instance.SignIn(EmailEntry.Text, PasswordEntry.Text);
		if (credential is null)
		{
			await Application.Current.MainPage.DisplayAlert("Autenticazione non riuscita", "Assicurati di aver compilato correttamente i campi.", "OK");
			Loading.IsVisible = false;
			return;
		}

		App.Current.MainPage = new ShellViewWin(_shellViewModelWin);
		Loading.IsVisible = false;
	}
}