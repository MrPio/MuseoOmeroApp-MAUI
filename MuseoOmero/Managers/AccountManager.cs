using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

namespace MuseoOmero.Managers;

public class AccountManager
{
	private static AccountManager _instance;
	private AccountManager() { }
	public static AccountManager Instance
	{
		get
		{
			_instance ??= new AccountManager();
			return _instance;
		}
	}

	public Dipendente Dipendente;
	public string Uid;
	private UserCredential UserCredential;

	public FirebaseAuthClient FirebaseAuthClient = new(
		new FirebaseAuthConfig()
		{
			ApiKey = " AIzaSyDXVjf6156a6Nh5cPyg2kQMgke1o0ykaxM",
			AuthDomain = "museoomero-ca8aa.firebaseapp.com",
			Providers = new FirebaseAuthProvider[] { new EmailProvider() },
			UserRepository = new FileUserRepository("MuseoOmero") // persist data into %AppData%\MuseoOmero
		}
	);

	public async Task<UserCredential> SignIn(string email, string password)
	{
		UserCredential = await FirebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
		Uid = UserCredential.User.Uid;
		Dipendente = await DatabaseManager.Instance.LoadJsonObject<Dipendente>($"dipendenti/{Uid}");

		if (Dipendente is null)
		{
			if (await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{Uid}") is { })
			{

				await App.Current.MainPage.DisplayAlert("Attenzione", "Non puoi accedere al portale gestionale utilizzando le credenziali di un cliente! Per favore inserisci delle credenziali valide.", "Ok");
				throw new Exception("Account di cliente");
			}
			else
			{
				await App.Current.MainPage.DisplayAlert("Errore", "L'account è stato rimosso dal database, quindi non è possibile accedere. Per favore contatta l'assistenza per la creazione di un nuovo account.", "Ok");
				throw new Exception("Account rimosso");

			}
			return null;
		}
		await SecureStorage.Default.SetAsync("uid", UserCredential.User.Uid);

		return UserCredential;
	}
	public void DeleteCache()
	{
		SecureStorage.Default.Remove("uid");
	}
	public async Task<bool> CacheSignIn()
	{
		var uid = await SecureStorage.Default.GetAsync("uid");

		if (uid is { })
		{
			try
			{
				Dipendente = await DatabaseManager.Instance.LoadJsonObject<Dipendente>($"dipendenti/{uid}");
				if (Dipendente is null)
					return false;
				Uid = uid;
			}
			catch (Exception e) { return false; }
			return true;
		}
		return false;
	}

	public async Task ResetPassword(string email)
	{
		await FirebaseAuthClient.ResetEmailPasswordAsync(email);
	}

	public async Task SignUp(string email, string password, string username)
	{
		UserCredential = await FirebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
	}
}