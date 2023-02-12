using MuseoOmero.ViewModelWin;

namespace MuseoOmero.ViewWin;

public partial class OpereViewWin : ContentPage
{
	public OpereViewWin()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var shellViewModelWin = (ShellViewModelWin)Parent.Parent.Parent.Parent.BindingContext;
        shellViewModelWin.ChangeIndex(3);
    }
}