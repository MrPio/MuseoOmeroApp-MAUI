using MuseoOmero.ViewModelWin;

namespace MuseoOmero.ViewWin;

public partial class ShellViewWin : Shell
{
	public ShellViewWin()
	{
		BindingContext = new ShellViewModelWin();
		InitializeComponent();
		InitRoutes();
	}
	private void InitRoutes()
	{
		Routing.RegisterRoute(nameof(HomeViewWin), typeof(HomeViewWin));
	}

	async void OnMenuItemChanged(object sender, CheckedChangedEventArgs e)
	{
		var vm = (ShellViewModelWin)BindingContext;
		if (!String.IsNullOrEmpty(vm.SelectedRoute))
			await Shell.Current.GoToAsync($"//{vm.SelectedRoute}");
	}
}