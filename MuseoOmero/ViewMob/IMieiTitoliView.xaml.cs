namespace MuseoOmero.ViewMob;

public partial class IMieiTitoliView : ContentView
{
    public IMieiTitoliView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        ((IMieiTitoliViewModel)BindingContext).view = this;
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var mainPageViewModel = (MainViewModel)Parent.Parent.Parent.Parent.Parent.BindingContext;
        mainPageViewModel.WavesExpandFactor = e.ScrollY / 160d;
    }
}