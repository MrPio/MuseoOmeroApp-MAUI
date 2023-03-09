namespace MuseoOmero.ViewModelMob;
public partial class IMieiTitoliViewModel : ObservableObject
{
	private MainViewModel _mainViewModel => App.Current.Handler.MauiContext.Services.GetService<MainViewModel>();
	private IDisposable _bigliettiObserver;

	[ObservableProperty]
	ObservableCollection<BigliettoViewModel> biglietti = new();

	public Biglietto Tmp => new("85293e2dewcdewds3ewdsqewds3rewd3rfwed852",DateTime.Now, DateTime.Now.AddDays(1), TipoBiglietto.Mostra, DateTime.Now.AddHours(32), TimeSpan.FromHours(14.24));

	public bool NoBiglietti
	{
		get
		{
			return Biglietti.Count == 0;
		}
	}

	[RelayCommand]
	public async void AggiornaClicked()
	{
		_mainViewModel.IsBusy = true;
		AccountManager.Instance.Utente = await DatabaseManager.Instance.LoadJsonObject<Utente>($"utenti/{AccountManager.Instance.Uid}");
		FetchBiglietti();
		_mainViewModel.IsBusy = false;
	}

	public void FetchBiglietti()
	{
		Biglietti.Clear();
		AccountManager.Instance.Utente.Biglietti.OrderByDescending(b => b.DataValidita)
			.ToList().ForEach(b => Biglietti.Add(new(b)));
		OnPropertyChanged(nameof(NoBiglietti));
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
				FetchBiglietti();
			}
		);
	}
}
