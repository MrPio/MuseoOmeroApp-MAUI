namespace MuseoOmero.ViewModelWin;

public partial class OpereViewModelWin : ObservableObject
{
	private HomeViewModelWin _homeViewModel;

	[ObservableProperty]
	string filtroOpere = "Titolo", filtroMostre = "Titolo";

	[ObservableProperty]
	ObservableCollection<Opera> opereOrdinate = new();

	[ObservableProperty]
	ObservableCollection<Mostra> mostreOrdinate = new();

	[ObservableProperty]
	bool opereSortAcending = true, mostreSortAcending = true;

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
		Func<Mostra, dynamic> mostreFunc = FiltroMostre switch
		{
			_ => o => o.Titolo
		};

		var opereOrdinate = _homeViewModel.Opere.OrderBy(opereFunc).ToList();
		if (!OpereSortAcending)
			opereOrdinate.Reverse();
		OpereOrdinate = new ObservableCollection<Opera>(opereOrdinate.ToList());


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
