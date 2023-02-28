using LiveChartsCore.SkiaSharpView.Maui;

namespace MuseoOmero.ViewWin;

public partial class StatisticheViewWin : ContentPage
{
	private StatisticheViewModelWin _viewModel;
	public StatisticheViewWin(StatisticheViewModelWin viewModel)
	{
		viewModel.Initialize();
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Pie1.Series = _viewModel.Series;
	}
}