namespace MuseoOmero.ViewModelMob.Templates
{
    public partial class WavesViewModel : ObservableObject
    {
        [ObservableProperty]
        GridLength topWave = 114, bottomWave = 44, intersection = 10;

        [ObservableProperty]
        double topWaveTranslationY = 0, bottomWaveTranslationY = 0;
    }
}
