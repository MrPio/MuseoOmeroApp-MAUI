
using MuseoOmero.Managers;
using MuseoOmero.ViewModel;
using Sharpnado.Tabs;


namespace MuseoOmero.View;

public partial class MainView : ContentPage
{
    public MainViewModel ViewModel = new();
    public DatabaseManager DatabaseManager { get => DatabaseManager.Instance; }
    private DeviceManager DeviceManager { get => DeviceManager.Instance; }

    int _previousIndex = -1;
    public MainView()
    {
        this.BindingContext = ViewModel;
        InitializeComponent();
        //TouchEffect.SetColor(tab1, Colors.Transparent);
        //TouchEffect.SetColor(tab2, Colors.Transparent);
        //TouchEffect.SetColor(tab3, Colors.Transparent);
        //TouchEffect.SetColor(tab4, Colors.Transparent);

        // Carico i biglietti dal servizio
        //ViewModel.IMieiTitoliViewModel.Biglietti =
        //    new ObservableCollection<BigliettoViewModel>(service.LoadBiglietti());

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
                //    {0,1,new Animation(v =>ViewModel.WavesViewModel.TopWave = new GridLength(v), ViewModel.WavesViewModel.  TopWave.Value, 56,Easing.CubicInOut)},
                //    { 0,0.5,new Animation(v => ViewModel.TopBarViewModel.RicercaOpacity = v, ViewModel.TopBarViewModel.RicercaOpacity, 0,Easing.CubicInOut )}
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

        //var animation = new Animation
        //{
        //    {0,1,new Animation(v =>ViewModel.WavesViewModel.TopWave =
        //    new GridLength(v), ViewModel.WavesViewModel.TopWave.Value, 114,Easing.CubicInOut)},
        //    { 0.5,1,new Animation(v => ViewModel.TopBarViewModel.RicercaOpacity =
        //    v, ViewModel.TopBarViewModel.RicercaOpacity, 1,Easing.CubicInOut )}
        //};
        //var c = DeviceDisplay.MainDisplayInfo;
        //animation.Commit(this, "TopAnimation", 16, 850, null, null);
    }
    private void ResetTopBarAndWaves()
    {
        //var animation = new Animation
        //        {
        //            {0,1,new Animation(v =>ViewModel.WavesViewModel.TopWaveTranslationY = v, ViewModel.WavesViewModel.TopWaveTranslationY, 0,Easing.CubicInOut)},
        //            {0,1,new Animation(v =>ViewModel.WavesViewModel.BottomWaveTranslationY = v, ViewModel.WavesViewModel.BottomWaveTranslationY, 0,Easing.CubicInOut)},
        //            {0,1,new Animation(v =>ViewModel.TopBarViewModel.TranslationY = v, ViewModel.TopBarViewModel.TranslationY, 0,Easing.CubicInOut)},
        //            {0,1,new Animation(v =>ViewModel.BottomBarTranslationX = v, ViewModel.BottomBarTranslationX, 0,Easing.CubicInOut)},
        //            {0,1,new Animation(v =>ViewModel.BottomBarOpacity = v, Math.Max(0,ViewModel.BottomBarOpacity), 1,Easing.CubicInOut)},
        //            {0,1,new Animation(v =>ViewModel.TopBarViewModel.Opacity = v, Math.Max(0,ViewModel.TopBarViewModel.Opacity), 1,Easing.CubicInOut)},
        //        };
        //var c = DeviceDisplay.MainDisplayInfo;
        //animation.Commit(this, "ResetAnimation", 16, 850, null, null);
    }
    private void TabsTransition(bool left)
    {
        //var w = DeviceManager.Width;
        //var parameters = left ? new[] { 0, -w, w, 0 } : new[] { -w, 0, 0, w };
        //var animation = new Animation
        //{
        //    {0,1,new Animation(v =>TopAndBottomWaves.TranslationX = v, parameters[0], parameters[1],Easing.CubicInOut)},
        //            { 0,1,new Animation(v => TopAndBottomWaves2.TranslationX = v, parameters[2], parameters[3],Easing.CubicInOut )}
        //        };
        //var c = DeviceDisplay.MainDisplayInfo;
        //animation.Commit(this, "WavesAnimation", 16, 500, null, null);
    }
}

