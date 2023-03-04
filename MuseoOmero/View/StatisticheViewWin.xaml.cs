using LiveChartsCore.SkiaSharpView.Maui;

namespace MuseoOmero.ViewWin;

public partial class StatisticheViewWin : ContentPage
{
	private StatisticheViewModelWin _viewModel;
	private double lastScrollY;
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
		_viewModel.Initialize();
	}

	private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
	{
		var scroll = ((ScrollView)sender).ScrollY;
		var absScrolled = Math.Abs(lastScrollY - scroll);
		if (absScrolled > 4)
		{
			var delta = lastScrollY - scroll > 0 ? 0.4d : -0.4d;
			delta *= absScrolled / 4;
			Pie1.InitialRotation += delta;
			Pie2.InitialRotation += delta;
			Pie3.InitialRotation += delta;
			Pie4.InitialRotation += delta;
			Pie5.InitialRotation += delta;
			Pie6.InitialRotation += delta;
			Polar1.InitialRotation += delta/8;
			lastScrollY = scroll;
		}
	}
}