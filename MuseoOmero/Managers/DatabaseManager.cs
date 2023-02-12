using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using MuseoOmero.Model.Enums;
using MuseoOmero.Models;
using MuseoOmero.ViewModel.Templates;


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
	public async void Save(string resource, object obj)
	{
		await FirebaseClient.Child("users").Child(AccountManager.Instance.Username)
			.Child(resource).PostAsync(obj);
	}

	public async Task<IEnumerable<T>> Load<T>(string resource)
	{
		var collection = await FirebaseClient.Child("users").Child(AccountManager.Instance.Username)
			.Child(resource).OnceAsync<T>();
		return from t in collection select (t.Object);
	}

	public void Observe<T>(string resource, Action<FirebaseEvent<T>> callback)
	{
		FirebaseClient.Child("users").Child(AccountManager.Instance.Username).Child(resource).AsObservable<T>().Subscribe(callback);
	}


	//public async Task<IEnumerable<BigliettoViewModel>> LoadBiglietti(DateTime data = default)
	//{
	//	var dataScelta = data == default ? DateTime.Today : data;
	//	//TODO
	//	await Task.Delay(1000);
	//	var biglietti = new List<Biglietto>()
	//		{
	//			new Biglietto(
	//				dataAcquisto:DateTime.ParseExact("10/10/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				dataValidita:DateTime.Today,
	//				tipologia:TipoBiglietto.MuseoAperto,
	//				dataGuida:null
	//				),
	//			new Biglietto(
	//				dataAcquisto:DateTime.ParseExact("10/10/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				dataValidita:DateTime.ParseExact("10/12/2022","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				tipologia:TipoBiglietto.Mostra,
	//				dataGuida:DateTime.Now
	//				),
	//			new Biglietto(
	//				dataAcquisto:DateTime.ParseExact("10/10/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				dataValidita:DateTime.ParseExact("10/12/2022","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				tipologia:TipoBiglietto.Laboratorio,
	//				dataGuida:null
	//				),
	//			new Biglietto(
	//				dataAcquisto:DateTime.ParseExact("10/10/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				dataValidita:DateTime.ParseExact("10/12/2022","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				tipologia:TipoBiglietto.Mostra,
	//				dataGuida:null
	//				),
	//			new Biglietto(
	//				dataAcquisto:DateTime.ParseExact("10/10/2010","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				dataValidita:DateTime.ParseExact("10/12/2022","dd/MM/yyyy",CultureInfo.InvariantCulture),
	//				tipologia:TipoBiglietto.Mostra,
	//				dataGuida:DateTime.Now
	//				),
	//		};
	//	return from biglietto in biglietti select new BigliettoViewModel(biglietto);

	//}
}
