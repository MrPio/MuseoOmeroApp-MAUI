using Mopups.Interfaces;

namespace MuseoOmero.ViewMob;

public partial class HomeView : ContentView
{
	private HomeViewModel viewModel => (HomeViewModel)BindingContext;
	private IPopupNavigation _popupNavigation => Service.Get<IPopupNavigation>();

	public HomeView()
	{
		InitializeComponent();
	}

	private void Opera_Clicked(object sender, EventArgs e)
	{
		var opera = ((VisualElement)sender).BindingContext as Opera;
		opera.Visualizzazioni++;
		DatabaseManager.Instance.Put($"opere/{opera.Id}/visualizzazioni", opera.Visualizzazioni);
		var popup = new OperaView() { BindingContext = opera };
		_popupNavigation.PushAsync(popup);
		viewModel.FetchOpere();
	}

	private void Mostra_Clicked(object sender, EventArgs e)
	{
		var mostra = ((VisualElement)sender).BindingContext as Mostra;
		var popup = new MostraView() { BindingContext = mostra };
		_popupNavigation.PushAsync(popup);
	}
}