
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

	public string Username = "MrPio";

	public bool Login()
	{
		return false;

	}

}
