namespace MuseoOmero.View.TemplatesWin;
public partial class PanoramicaElementViewWin : ContentView
{
	public PanoramicaElementViewWin()
	{
		InitializeComponent();
	}

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		var vm = BindingContext as PanoramicaElementViewModelWin;

		if (!string.IsNullOrEmpty(vm.Route))
		{
			var shellViewModel = this.Handler.MauiContext.Services.GetService<ShellViewModelWin>();
			shellViewModel.SelectedRoute = vm.Route;
		}
	}
}