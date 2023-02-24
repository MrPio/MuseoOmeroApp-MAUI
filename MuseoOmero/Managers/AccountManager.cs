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

	public Utente Account;
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
		try
		{
			UserCredential = await FirebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);
			Uid = UserCredential.User.Uid;
			Account = await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{Uid}");
			await SecureStorage.Default.SetAsync("uid", UserCredential.User.Uid);
		}
		catch (FirebaseAuthException e) { return null; }
		return UserCredential;
	}

	public async Task<bool> CacheSignIn()
	{
		var uid = await SecureStorage.Default.GetAsync("uid");

		if (uid is { })
		{
			try
			{
				Account = await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{uid}");
				Uid = uid;
			}
			catch (Exception e) { return false; }
			return true;
		}
		return false;
	}

	public async Task SignUp(string email, string password, string username)
	{
		UserCredential = await FirebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
	}
}