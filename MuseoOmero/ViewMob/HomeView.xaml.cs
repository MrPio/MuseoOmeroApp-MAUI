namespace MuseoOmero.ViewMob;

public partial class HomeView : ContentView
{
	private HomeViewModel viewModel => (HomeViewModel)BindingContext;
	public HomeView()
	{
		InitializeComponent();
	}
}