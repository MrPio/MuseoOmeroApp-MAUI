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
	public Utente Utente;
	public string Uid;
	private UserCredential UserCredential;

	public FirebaseAuthClient FirebaseAuthClient = new(
		new FirebaseAuthConfig()
		{
			ApiKey = " AIzaSyDXVjf6156a6Nh5cPyg2kQMgke1o0ykaxM",
			AuthDomain = "museoomero-ca8aa.firebaseapp.com",
			Providers = new FirebaseAuthProvider[] { new EmailProvider(), new GoogleProvider() },
			UserRepository = new FileUserRepository("MuseoOmero") // persist data into %AppData%\MuseoOmero
		}
	);

	//Dipendenti
	public async Task<UserCredential> SignIn(string email, string password)
	{
		UserCredential = await FirebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
		Uid = UserCredential.User.Uid;
		Dipendente = await DatabaseManager.Instance.LoadJsonObject<Dipendente>($"dipendenti/{Uid}");

		if (Dipendente is null)
			if (await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{Uid}") is { })
				throw new Exception("Account di cliente");
			else
				throw new Exception("Account rimosso");
		await SecureStorage.Default.SetAsync("uid", Uid);

		return UserCredential;
	}

	//Clienti
	public async Task<UserCredential> SignInCliente(string email, string password)
	{
		UserCredential = await FirebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
		Uid = UserCredential.User.Uid;
		Utente = await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{Uid}");

		if (Utente is null)
			throw new Exception("Account rimosso");

		await SecureStorage.Default.SetAsync("uid", Uid);
		Utente.LastOnline = DateTime.Now;
		await DatabaseManager.Instance.Put($"utenti/{Uid}/last_online", Utente.LastOnline);
		return UserCredential;
	}
	public async Task<UserCredential> SignInWithGoogle(string email, string password)
	{
		// NON HO LA MINIMA IDEA DI COME SI IMPLEMENTI

		//var credential=GoogleProvider.GetCredential()
		//UserCredential = await FirebaseAuthClient.SignInWithCredentialAsync(credential);
		return null;
	}
	public async Task<UserCredential> SignUpCliente(string email, string password, string username, string nome, string cognome, string cellulare)
	{
		UserCredential = await FirebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password);
		Uid = UserCredential.User.Uid;
		Utente = new(Uid, username, nome, cognome, cellulare, new(), new(), null, DateTime.Now);
		await DatabaseManager.Instance.Put($"utenti/{Uid}", Utente);

		await SecureStorage.Default.SetAsync("uid", Uid);
		return UserCredential;
	}


	public void DeleteCache()
	{
		SecureStorage.Default.Remove("uid");
	}
	public async Task<bool> CacheSignIn(bool dipendente = true)
	{
		var uid = await SecureStorage.Default.GetAsync("uid");

		if (uid is { })
		{
			try
			{
				if (dipendente)
				{
					Dipendente = await DatabaseManager.Instance.LoadJsonObject<Dipendente>($"dipendenti/{uid}");
					if (Dipendente is null)
						return false;
				}
				else
				{
					Utente = await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{uid}");
					if (Utente is null)
						return false;
					Utente.LastOnline = DateTime.Now;
					await DatabaseManager.Instance.Put($"utenti/{uid}/last_online", Utente.LastOnline);
				}
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
	public async Task LoadDipendente()
	{
		Dipendente = await DatabaseManager.Instance.LoadJsonObject<Dipendente>($"dipendenti/{Uid}");
	}
	public async Task ResetPassword()
	{
		var main = App.Current.MainPage;
		var email = await main.DisplayPromptAsync("Email", "Inserisci l'email dell'account dove invieremo la procedura necessaria al ripristino della password.", placeholder: "email@example.com", maxLength: 50);

		if (!String.IsNullOrEmpty(email))
		{
			if (email.Contains('@') && email.Contains('.'))
			{
				await AccountManager.Instance.ResetPassword(email);
				await main.DisplayAlert("Email inviata", "Per favore, controlla la tua casella di posta per reimpostare la password.", "Ok");

			}
			else
			{
				await main.DisplayAlert("Attenzione", "Per favore, inserisci un'email valida.", "Riprova");
				await ResetPassword();
			}
		}
	}
}