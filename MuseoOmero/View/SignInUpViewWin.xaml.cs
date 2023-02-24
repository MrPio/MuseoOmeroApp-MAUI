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
	private async void Accedi_Clicked(object sender, EventArgs e)
	{
		Loading.IsVisible = true; 
		//if (EmailEntry.Text.Length < 3 || PasswordEntry.Text.Length < 3)
		//{
		//	await Application.Current.MainPage.DisplayAlert("Campi non compilati", "Assicurati di aver compilato correttamente i campi.", "OK");
		//	Loading.IsVisible = false;
		//	return;
		//}
		//var credential = await AccountManager.Instance.SignIn(EmailEntry.Text, PasswordEntry.Text);
		//if (credential is null)
		//{
		//	await Application.Current.MainPage.DisplayAlert("Autenticazione non riuscita", "Assicurati di aver compilato correttamente i campi.", "OK");
		//	Loading.IsVisible = false;
		//	return;
		//}
		App.Current.MainPage = new ShellViewWin(_shellViewModelWin);
		Loading.IsVisible = false;

	}
}