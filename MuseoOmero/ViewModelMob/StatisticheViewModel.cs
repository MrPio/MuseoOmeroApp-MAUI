using System.Linq;

namespace MuseoOmero.ViewModelMob;
public partial class StatisticheViewModel : ObservableObject
{
	MainViewModel mainViewModel => Service.Get<MainViewModel>();
	[ObservableProperty]
	ObservableCollection<Visita> visite = new();

	[ObservableProperty]
	bool noVisite = true, isRefreshing;

	[RelayCommand]
	async void Refresh()
	{
		mainViewModel.IsBusy = true;
		await DatabaseManager.Instance.ReloadUtente();
		Initialize();
		mainViewModel.IsBusy = false;
		IsRefreshing = false;
	}

	public void Initialize()
	{
		var utente = AccountManager.Instance.Utente;
		var visite = utente.Biglietti.Where(b => b.IsConvalidato).Select(b => new Visita(b.DataValidita, b.Tipologia, b.OrarioGuida is { })).ToList();
		foreach (var (q, v) in from q in utente.Questionari
							   from v in visite
							   where q.DataVisita is { } && v.Data.Date == q.DataVisita?.Date
							   select (q, v))
		{
			v.Questionario = q;
		}
		NoVisite = visite.Count == 0;
		Visite = new(visite);
	}
}
public class Visita
{
	public DateTime Data { get; set; }
	public TipoBiglietto Tipologia { get; set; }
	public bool ConGuida { get; set; }
	public Questionario? Questionario { get; set; }

	public Visita(DateTime data, TipoBiglietto tipologia, bool conGuida, Questionario questionario = null)
	{
		Data = data;
		Tipologia = tipologia;
		ConGuida = conGuida;
		Questionario = questionario;
	}
}
