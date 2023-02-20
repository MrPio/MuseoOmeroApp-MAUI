using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using MuseoOmero.Model.Enums;
using MuseoOmero.Model;
using MuseoOmero.ViewModel.Templates;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MuseoOmero.Managers;
public class DatabaseManager
{
	private static DatabaseManager _instance;
	private DatabaseManager() { }
	public static DatabaseManager Instance
	{
		get
		{
			_instance ??= new DatabaseManager();
			return _instance;
		}
	}

	public FirebaseClient FirebaseClient = new FirebaseClient("https://museoomero-ca8aa-default-rtdb.europe-west1.firebasedatabase.app/");

	// PERFETTAMENTE FUNZIONANTE!
	private ChildQuery GetChild(string resource)
	{
		ChildQuery fc = null;
		foreach (var child in resource.Split('/', StringSplitOptions.RemoveEmptyEntries))
			fc = fc is null ? FirebaseClient.Child(child) : fc.Child(child);
		return fc;
	}

	public async Task Put(string resource, object obj)
	{
		await GetChild(resource).PutAsync(obj);
	}
	public async Task Post(string resource, object obj)
	{
		await GetChild(resource).PostAsync(obj);
	}

	public async Task<List<T>> LoadJsonArray<T>(string resource)
	{
		var collection = await GetChild(resource).OnceAsJsonAsync();
		var dict = JsonConvert.DeserializeObject<Dictionary<string, T>>(collection);
		if (dict is null)
			return null;
		if (typeof(T) == typeof(Utente))
			foreach (var entry in dict)
				(entry.Value as Utente).Id = entry.Key;
		if (typeof(T) == typeof(Opera))
			foreach (var entry in dict)
				(entry.Value as Opera).Id = entry.Key;

		return (from entry in dict select entry.Value).ToList();
	}
	public async Task<T> LoadJsonObject<T>(string resource)
	{
		var collection = await GetChild(resource).OnceAsJsonAsync();
		return JsonConvert.DeserializeObject<T>(collection);
	}

	public void Observe<T>(string resource, Action<FirebaseEvent<T>> callback)
	{
		GetChild(resource).AsObservable<T>().Subscribe(callback);
	}

	public async Task SaveAccount(Utente account)
	{
		await Put($"utenti/{account.Id}/", account);
	}
}
