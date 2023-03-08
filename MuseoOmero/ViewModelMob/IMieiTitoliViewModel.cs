namespace MuseoOmero.ViewModelMob;
public partial class IMieiTitoliViewModel : ObservableObject
{
	private MainViewModel _mainViewModel => App.Current.Handler.MauiContext.Services.GetService<MainViewModel>();
	private IDisposable _bigliettiObserver;

	[ObservableProperty]
	ObservableCollection<BigliettoViewModel> biglietti = new();

	[RelayCommand]
	public async void AggiornaClicked()
	{
		_mainViewModel.IsBusy = true;
		AccountManager.Instance.Utente = await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{AccountManager.Instance.Uid}");
		Initialize();
		_mainViewModel.IsBusy = false;
	}


	public void Initialize()
	{
		Biglietti = new();
		AccountManager.Instance.Utente.Biglietti.ForEach(b => Biglietti.Add(new(b)));
		if (Biglietti.Count == 0)
			Biglietti = null;
		Debug.Print(Biglietti.Count.ToString());
	}

	public void ObserveBiglietti()
	{
		// LA LISTA SI AGGIORNA, MA OVVIAMENTE LA COLLECTIONVIEW NO
		_bigliettiObserver = DatabaseManager.Instance.Observe<Biglietto>(
			resource: $"utenti/{AccountManager.Instance.Uid}/biglietti",
			callback: (o) =>
			{
				var utente = AccountManager.Instance.Utente;
				var msg = o.Object;
				if (o.Object is null)
					return;
				// Se l'utente ha modificato il testo di un messaggio che già è nella collection view
				if (utente.Biglietti.Find(b => b.DataAcquisto == msg.DataAcquisto) is { } b)
					utente.Biglietti[utente.Biglietti.IndexOf(b)] = msg;
				else
					utente.Biglietti.Add(msg);
				Initialize();
			}
		);
	}
}
