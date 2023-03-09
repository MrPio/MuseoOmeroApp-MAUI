using Mopups.Interfaces;

namespace MuseoOmero.ViewMob.Popups;

public partial class BigliettoPopupView : Mopups.Pages.PopupPage
{
	private readonly IPopupNavigation _popupNavigation;

	public BigliettoPopupView(IPopupNavigation popupNavigation)
	{
		_popupNavigation = popupNavigation;
		BindingContext = this;
		InitializeComponent();
	}

	private async  void ExitLabel_Tapped(object sender, EventArgs e)
	{
		await _popupNavigation.PopAllAsync();
	}
}