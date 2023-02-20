namespace MuseoOmero.ViewWin;

public partial class HomeViewWin : ContentPage
{
	private readonly HomeViewModelWin _viewModel = new();
	public HomeViewWin()
	{
		BindingContext = _viewModel;
		_viewModel.Initialize();
		InitializeComponent();
		Picker_SelectedIndexChanged(RepartoPicker, null);
	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (sender == RepartoPicker)
		{
			var elems = RepartoPicker.SelectedIndex == 0 ? _viewModel.Sale : _viewModel.NomiMostre;
			SalaMostraPicker.ItemsSource = elems;
			SalaMostraPicker.ItemsSource = SalaMostraPicker.GetItemsAsArray(); //https://github.com/dotnet/maui/issues/9739
			if (elems.Count > 0)
				SalaMostraPicker.SelectedIndex = 0;
		}
		_viewModel.FiltraOpere();
	}
}