namespace MuseoOmero.ViewWin;

public partial class OpereViewWin : ContentPage
{
	private OpereViewModelWin _viewModel;
	public OpereViewWin(OpereViewModelWin viewModel)
	{
		_viewModel = viewModel;
		_viewModel.Initialize();
		InitializeComponent();
	}
}