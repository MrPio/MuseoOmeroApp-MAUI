using Sharpnado.Tabs;
using Sharpnado.Tabs.Effects;

namespace MuseoOmero.ViewMob;

public partial class MainView : ContentPage
{
	private readonly MainViewModel _viewModel = new();
	public DatabaseManager DatabaseManager { get => DatabaseManager.Instance; }
	private DeviceManager DeviceManager { get => DeviceManager.Instance; }

	int _previousIndex = -1;
	public MainView(MainViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
		TouchEffect.SetColor(Tab1, Colors.Transparent);
		TouchEffect.SetColor(Tab2, Colors.Transparent);
		TouchEffect.SetColor(Tab3, Colors.Transparent);
		TouchEffect.SetColor(Tab4, Colors.Transparent);
		TouchEffect.SetColor(Tab5, Colors.Transparent);
		_viewModel.Initialize();
		TopBarFilterShowHide(false);
	}

	private void Switcher_PropertyChanging(object _, PropertyChangingEventArgs e)
	{
		if (Switcher is{ } && e.PropertyName == "SelectedIndex")
			_previousIndex = Switcher.SelectedIndex;
	}
	private void Switcher_PropertyChanged(object _, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (Switcher is { } && e.PropertyName == "SelectedIndex" && _previousIndex != -1)
		{
			var currentIndex = Switcher.SelectedIndex;

			//Some tabs want filter on the tob bar
			TopBarFilterShowHide(new[] { 1 }.Contains(currentIndex));

			if (currentIndex != _previousIndex)
				ResetTopBarAndWaves();

			if (currentIndex > _previousIndex)
				TabsTransition(true);
			else if (currentIndex < _previousIndex)
				TabsTransition(false);
		}
	}

	private void TopBarFilterShowHide(bool show)
	{
		var waves = _viewModel.WavesViewModel;
		var topBar = _viewModel.TopBarViewModel;
		var anim = new[]
		{
			new[]{ 56d, 114d},
			new[]{ 0d, 1d},
			new[]{ 0+(show?0.5:0), 0.5 +( show ? 0.5 : 0 )}
		};

		var animation = new Animation
		{
			{0,1,new Animation(v =>waves.TopWave =
			new GridLength(v), waves.TopWave.Value, anim[0][show?1:0],Easing.CubicOut)},
			{ anim[2][0],anim[2][1],new Animation(v => topBar.RicercaOpacity =
			v, topBar.RicercaOpacity, anim[1][show?1:0],Easing.CubicOut )}
		};
		animation.Commit(this, "TopAnimation", 16, 850, null, null);
	}
	private void ResetTopBarAndWaves()
	{
		var animation = new Animation
				{
					{0,1,new Animation(v =>_viewModel.WavesViewModel.TopWaveTranslationY = v, _viewModel.WavesViewModel.TopWaveTranslationY, 0,Easing.CubicOut)},
					{0,1,new Animation(v =>_viewModel.WavesViewModel.BottomWaveTranslationY = v, _viewModel.WavesViewModel.BottomWaveTranslationY, 0,Easing.CubicOut)},
					{0,1,new Animation(v =>_viewModel.TopBarViewModel.TranslationY = v, _viewModel.TopBarViewModel.TranslationY, 0,Easing.CubicOut)},
					{0,1,new Animation(v =>_viewModel.BottomBarTranslationX = v, _viewModel.BottomBarTranslationX, 0,Easing.CubicOut)},
					{0,1,new Animation(v =>_viewModel.BottomBarOpacity = v, Math.Max(0,_viewModel.BottomBarOpacity), 1,Easing.CubicOut)},
					{0,1,new Animation(v =>_viewModel.TopBarViewModel.Opacity = v, Math.Max(0,_viewModel.TopBarViewModel.Opacity), 1,Easing.CubicOut)},
				};
		var c = DeviceDisplay.MainDisplayInfo;
		animation.Commit(this, "ResetAnimation", 16, 850, null, null);
	}
	private void TabsTransition(bool left)
	{
		var w = DeviceManager.Width;
		var parameters = left ? new[] { 0, -w, w, 0 } : new[] { -w, 0, 0, w };
		var animation = new Animation
		{
			{0,1,new Animation(v =>TopAndBottomWaves.TranslationX = v, parameters[0], parameters[1],Easing.CubicOut)},
					{ 0,1,new Animation(v => TopAndBottomWaves2.TranslationX = v, parameters[2], parameters[3],Easing.CubicOut )}
				};
		var c = DeviceDisplay.MainDisplayInfo;
		animation.Commit(this, "WavesAnimation", 16, 500, null, null);
	}
}

