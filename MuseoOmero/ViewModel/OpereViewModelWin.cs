namespace MuseoOmero.ViewModelWin;

partial class OpereViewModelWin: ObservableObject
{
	private HomeViewModelWin _homeViewModel;
	[ObservableProperty]
	string filtroPickerPickerSelectedItem = "Nome";

	[ObservableProperty]
	ObservableCollection<Opera> opereFiltrate = new();

	[ObservableProperty]
	ObservableCollection<Mostra> mostreFiltrate = new();

	public void Initialize()
	{
		OpereFiltrate = new ObservableCollection<Opera>(_homeViewModel.Opere.OrderBy(o => o.Nome));
		MostreFiltrate = new ObservableCollection<Mostra>(_homeViewModel.Mostre.OrderBy(o => o.Titolo));
	}

	public OpereViewModelWin(HomeViewModelWin homeViewModelWin)
	{
		_homeViewModel=homeViewModelWin;
	}
}
