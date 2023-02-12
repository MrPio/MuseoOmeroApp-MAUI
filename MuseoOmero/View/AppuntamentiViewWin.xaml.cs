using MuseoOmero.ViewModelWin;

namespace MuseoOmero.ViewWin;

public partial class AppuntamentiViewWin : ContentPage
{
	public AppuntamentiViewWin()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var shellViewModelWin = (ShellViewModelWin)Parent.Parent.Parent.Parent.BindingContext;
        shellViewModelWin.ChangeIndex(4);
    }
}