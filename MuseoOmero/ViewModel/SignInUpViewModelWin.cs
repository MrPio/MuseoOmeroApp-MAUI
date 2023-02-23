using MuseoOmero.ViewModel.Templates;

namespace MuseoOmero.ViewModelWin;
public partial class SignInUpViewModelWin : ObservableObject
{
	[ObservableProperty]
	MyEntryViewModel emailEntryViewModel = new(
		placeholder: "",
		text: "",
		icon: IconFont.Email
	), passwordEntryViewModel = new(
		placeholder: "",
		text: "",
		icon: IconFont.Key,
		isPassword: true
	);

	[ObservableProperty]
	RoundedButtonViewModel signInViewModel = new(
		text: "Accedi",
		onClick: async () => { }
	);
	private ShellViewModelWin _shellViewModelWin;
	public SignInUpViewModelWin(ShellViewModelWin shellViewModelWin)
	{
		_shellViewModelWin = shellViewModelWin;
		SignInViewModel.OnClick = async() =>
		{
			await SignIn();
		};
	}

	async Task SignIn()
	{
		//if(EmailEntryViewModel.Text.Length<3 || PasswordEntryViewModel.Text.Length < 3)
		//{
		//	await Application.Current.MainPage.DisplayAlert("Campi non compilati", "Assicurati di aver compilato correttamente i campi.", "OK");
		//	return;
		//}
		//var credential = await AccountManager.Instance.SignIn(EmailEntryViewModel.Text, PasswordEntryViewModel.Text);
		//if(credential is null) {
		//	await Application.Current.MainPage.DisplayAlert("Autenticazione non riuscita", "Assicurati di aver compilato correttamente i campi.", "OK");
		//	return;
		//}

		//Application.Current.MainPage = new ShellViewWin(_shellViewModelWin);

		App.Current.MainPage = new ShellViewWin(_shellViewModelWin);
	}
}
