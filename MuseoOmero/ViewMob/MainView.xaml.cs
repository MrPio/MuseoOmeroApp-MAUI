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
		_viewModel=_viewModel;
		BindingContext = _viewModel;
        InitializeComponent();
        TouchEffect.SetColor(tab1, Colors.Transparent);
        TouchEffect.SetColor(tab2, Colors.Transparent);
        TouchEffect.SetColor(tab3, Colors.Transparent);
        TouchEffect.SetColor(tab4, Colors.Transparent);
    }

    private void Switcher_PropertyChanging(object sender, PropertyChangingEventArgs e)
    {
        if (e.PropertyName == "SelectedIndex")
        {
            _previousIndex = ((ViewSwitcher)sender).SelectedIndex;
        }
    }

    private void Switcher_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "SelectedIndex" && _previousIndex != -1)
        {
            var currentIndex = ((ViewSwitcher)sender).SelectedIndex;
            if (currentIndex == 0)
                OnIMieiTitoliAppearing();
            else
            {
                //Skia.IsVisible = false;
                //Skia.IsAnimationEnabled = false;

                //var animation = new Animation
                //{
                //    {0,1,new Animation(v =>_viewModel.Waves_viewModel.TopWave = new GridLength(v), _viewModel.Waves_viewModel.  TopWave.Value, 56,Easing.CubicInOut)},
                //    { 0,0.5,new Animation(v => _viewModel.TopBar_viewModel.RicercaOpacity = v, _viewModel.TopBar_viewModel.RicercaOpacity, 0,Easing.CubicInOut )}
                //};
                //var c = DeviceDisplay.MainDisplayInfo;
                //animation.Commit(this, "TopAnimation", 16, 850, null, null);
            }

            if (currentIndex != _previousIndex)
                ResetTopBarAndWaves();

            if (currentIndex > _previousIndex)
                TabsTransition(true);
            else if (currentIndex < _previousIndex)
                TabsTransition(false);
        }
    }

    private void OnIMieiTitoliAppearing()
    {
        //Skia.IsAnimationEnabled = true;
        //Skia.Source = new SKFileLottieImageSource { File = "air_ticket.json" };
        //Skia.IsVisible = true;

        var animation = new Animation
        {
            {0,1,new Animation(v =>_viewModel.WavesViewModel.TopWave =
            new GridLength(v), _viewModel.WavesViewModel.TopWave.Value, 114,Easing.CubicInOut)},
            { 0.5,1,new Animation(v => _viewModel.TopBarViewModel.RicercaOpacity =
            v, _viewModel.TopBarViewModel.RicercaOpacity, 1,Easing.CubicInOut )}
        };
        var c = DeviceDisplay.MainDisplayInfo;
        animation.Commit(this, "TopAnimation", 16, 850, null, null);
    }
    private void ResetTopBarAndWaves()
    {
        var animation = new Animation
                {
                    {0,1,new Animation(v =>_viewModel.WavesViewModel.TopWaveTranslationY = v, _viewModel.WavesViewModel.TopWaveTranslationY, 0,Easing.CubicInOut)},
                    {0,1,new Animation(v =>_viewModel.WavesViewModel.BottomWaveTranslationY = v, _viewModel.WavesViewModel.BottomWaveTranslationY, 0,Easing.CubicInOut)},
                    {0,1,new Animation(v =>_viewModel.TopBarViewModel.TranslationY = v, _viewModel.TopBarViewModel.TranslationY, 0,Easing.CubicInOut)},
                    {0,1,new Animation(v =>_viewModel.BottomBarTranslationX = v, _viewModel.BottomBarTranslationX, 0,Easing.CubicInOut)},
                    {0,1,new Animation(v =>_viewModel.BottomBarOpacity = v, Math.Max(0,_viewModel.BottomBarOpacity), 1,Easing.CubicInOut)},
                    {0,1,new Animation(v =>_viewModel.TopBarViewModel.Opacity = v, Math.Max(0,_viewModel.TopBarViewModel.Opacity), 1,Easing.CubicInOut)},
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
            {0,1,new Animation(v =>TopAndBottomWaves.TranslationX = v, parameters[0], parameters[1],Easing.CubicInOut)},
                    { 0,1,new Animation(v => TopAndBottomWaves2.TranslationX = v, parameters[2], parameters[3],Easing.CubicInOut )}
                };
        var c = DeviceDisplay.MainDisplayInfo;
        animation.Commit(this, "WavesAnimation", 16, 500, null, null);
    }
}

