namespace MuseoOmero.ViewModelMob.Templates
{
    public partial class TopBarViewModel : ObservableObject
    {
        [ObservableProperty]
        string title = "Home";

        [ObservableProperty]
        double ricercaOpacity = 1, translationY = 0, opacity = 1;

        [ObservableProperty]
        bool isDate, ricercaEnabled;
    }
}
