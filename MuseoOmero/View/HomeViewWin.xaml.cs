namespace MuseoOmero.ViewWin;

public partial class HomeViewWin : ContentPage
{
	public HomeViewWin()
	{
		BindingContext = new HomeViewModelWin();
		InitializeComponent();
	}
}