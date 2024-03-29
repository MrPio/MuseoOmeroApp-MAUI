﻿using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;

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
	public FirebaseClient FirebaseClient =
		new(
			"https://museoomero-ca8aa-default-rtdb.europe-west1.firebasedatabase.app/",
			new FirebaseOptions
			{
				AuthTokenAsyncFactory = () => {
					return AccountManager.Instance.FirebaseAuthClient.User.GetIdTokenAsync(); 
				}
			});
	public List<IDisposable> Subscribers = new();

	/* • Append -> Put($"utenti/{utenti[0].Id}/biglietti/{utenti[0].Biglietti.Count}/", biglietto);
	 */

	// Low-Level
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
	public async Task Delete(string resource)
	{
		await GetChild(resource).DeleteAsync();
	}
	public async Task<List<T>> LoadJsonArray<T>(string resource)
	{
		var collection = await GetChild(resource).OnceAsJsonAsync();
		var dict = JsonConvert.DeserializeObject<Dictionary<string, T>>(collection);
		if (dict is null)
			return null;
		if (typeof(T) == typeof(Utente))
			foreach (var entry in dict)
				(entry.Value as Utente).Uid = entry.Key;
		if (typeof(T) == typeof(Opera))
			foreach (var entry in dict)
				(entry.Value as Opera).Id = entry.Key;
		if (typeof(T) == typeof(Mostra))
			foreach (var entry in dict)
				(entry.Value as Mostra).Id = entry.Key;

		return (from entry in dict select entry.Value).ToList();
	}
	public async Task<T> LoadJsonObject<T>(string resource)
	{
		var child = GetChild(resource);
		var collection = await child.OnceAsJsonAsync();
		var obj = JsonConvert.DeserializeObject<T>(collection);
		if (obj is { })
		{
			if (typeof(T) == typeof(Dipendente))
				(obj as Dipendente).Uid = resource.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();
			if (typeof(T) == typeof(Utente))
				(obj as Utente).Uid = resource.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();

		}
		return obj;
	}
	public IDisposable Observe<T>(string resource, Action<FirebaseEvent<T>> callback)
	{
		var disposable = GetChild(resource).AsObservable<T>().Subscribe(callback);
		Subscribers.Add(disposable);
		return disposable;
	}

	// High-Level
	public async Task SaveUtente(Utente utente)
	{
		await Put($"utenti/{utente.Uid}/", utente);
	}
	public async Task ReloadUtente()
	{
		var utente = AccountManager.Instance.Utente;
		if(utente is { })
			AccountManager.Instance.Utente = await LoadJsonObject<Utente>($"utenti/{utente.Uid}");
	}

}
