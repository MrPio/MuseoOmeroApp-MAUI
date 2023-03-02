namespace MuseoOmero.ViewModelWin;

public partial class OpereViewModelWin : ObservableObject
{
	private HomeViewModelWin _homeViewModel;
	public bool ShowOpera = false;
	[ObservableProperty]
	string filtroOpere = "Titolo", filtroMostre = "Titolo";

	[ObservableProperty]
	ObservableCollection<Opera> opereOrdinate = new();

	[ObservableProperty]
	ObservableCollection<Mostra> mostreOrdinate = new();

	[ObservableProperty]
	bool opereSortAcending = true, mostreSortAcending = true;

	[ObservableProperty]
	bool opereOn = true, mostreOn, aggiungiMostreOn;

	[ObservableProperty]
	Opera selectedOpera,nuovaOpera;

	[ObservableProperty]
	Mostra selectedMostra,nuovaMostra;

	[RelayCommand]
	void HeaderLabelTap(string titolo)
	{
		if (FiltroOpere == titolo)
			OpereSortAcending = !OpereSortAcending;
		else
			OpereSortAcending = true;
		FiltroOpere = titolo;
		OrdinaOpere();
	}
	[RelayCommand]
	void InvertSortTap()
	{
		OpereSortAcending = !OpereSortAcending;
		OrdinaOpere();
	}

	[RelayCommand]
	void HeaderLabelMostreTap(string titolo)
	{
		if (FiltroMostre == titolo)
			MostreSortAcending = !MostreSortAcending;
		else
			MostreSortAcending = true;
		FiltroMostre = titolo;
		OrdinaMostre();
	}
	[RelayCommand]
	void InvertSortMostreTap()
	{
		MostreSortAcending = !MostreSortAcending;
		OrdinaMostre();
	}

	public void OrdinaOpere()
	{
		Func<Opera, dynamic> opereFunc = FiltroOpere switch
		{
			"DataReg" => o => o.DataAggiunta,
			"Sala" => o => o.Sala,
			"Autore" => o => o.Autore,
			"Visuals" => o => o.Visualizzazioni,
			_ => o => o.Nome
		};
		var opereOrdinate = _homeViewModel.Opere.OrderBy(opereFunc).ToList();
		if (!OpereSortAcending)
			opereOrdinate.Reverse();
		OpereOrdinate = new ObservableCollection<Opera>(opereOrdinate.ToList());
	}
	public void OrdinaMostre()
	{
		Func<Mostra, dynamic> mostreFunc = FiltroMostre switch
		{
			"DataReg" => m => m.DataAggiunta,
			"Sala" => m => m.DataInizio,
			"Autore" => m => m.DataFine,
			_ => m => m.Titolo
		};
		var mostreOrdinate = _homeViewModel.Mostre.OrderBy(mostreFunc).ToList();
		if (!MostreSortAcending)
			mostreOrdinate.Reverse();
		MostreOrdinate = new ObservableCollection<Mostra>(mostreOrdinate.ToList());
	}
	public OpereViewModelWin(HomeViewModelWin homeViewModelWin)
	{
		_homeViewModel = homeViewModelWin;
	}
}
