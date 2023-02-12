using MuseoOmero.ViewModelWin;

namespace MuseoOmero.ViewWin;

public partial class NegozioViewWin : ContentPage
{
	public NegozioViewWin()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var shellViewModelWin = (ShellViewModelWin)Parent.Parent.Parent.Parent.BindingContext;
        shellViewModelWin.ChangeIndex(2);
    }
}