namespace MuseoOmero.ViewModelMob;

public partial class HomeViewModel : ObservableObject
{
	private string _currentSala = "Ingresso";
	public string CurrentSala
	{
		get => _currentSala;
		set { _currentSala = value; OnPropertyChanged(nameof(CurrentSala)); FetchOpere(); }
	}

	[ObservableProperty]
	ObservableCollection<Opera> opereFiltrate = new();
	[ObservableProperty]
	ObservableCollection<Mostra> mostre = new();
	[ObservableProperty]
	bool isRefreshing;

	public void FetchOpere()
	{
		OpereFiltrate = new();
		var opere = Service.Get<MainViewModel>().Opere;
		foreach (var o in opere.Where(o => o.Sala == CurrentSala))
			OpereFiltrate.Add(o);
	}
	public void FetchMostre()
	{
		Mostre = new();
		var mostre = Service.Get<MainViewModel>().Mostre;
		foreach (var m in mostre)
			Mostre.Add(m);
	}

	[RelayCommand]
	async void Refresh()
	{
		await Service.Get<MainViewModel>().Initialize();
		IsRefreshing = false;
	}
}
