
using MuseoOmero.Model;
using Newtonsoft.Json;

namespace MuseoOmero.Managers;

class AccountManager
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

	[JsonProperty("account")] public Utente Account;
}
