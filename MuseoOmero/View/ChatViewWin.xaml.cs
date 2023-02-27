namespace MuseoOmero.ViewWin;

public partial class ChatViewWin : ContentPage
{
	ChatViewModelWin _viewModel;
	public ChatViewWin(ChatViewModelWin viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		_viewModel.Initialize();
		InitializeComponent();
	}

	private void HighlightView_Pressed(object sender, EventArgs e)
	{
		SendBorder.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => SendBorder.BackgroundColor = c, 350, Easing.CubicOut);
		SendIcon.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => SendIcon.TextColor = c, 350, Easing.CubicOut);

	}

	private void HighlightView_Released(object sender, EventArgs e)
	{
		SendBorder.CancelAnimation();
		SendIcon.CancelAnimation();
		SendBorder.ColorTo(DeviceManager.Instance.Colors[3], DeviceManager.Instance.Colors[0], c => SendBorder.BackgroundColor = c, 350, Easing.CubicOut);
		SendIcon.ColorTo(DeviceManager.Instance.Colors[0], DeviceManager.Instance.Colors[3], c => SendIcon.TextColor = c, 350, Easing.CubicOut);

	}

	private async void HighlightView_Clicked(object sender, EventArgs e)
	{
		var text = SendEntry.Text;
		if (text.Length > 0)
		{
			var msg = new MessaggioConMittente(new(DateTime.Now, text), false);
			_viewModel.Messaggi.Add(msg);
			//SendEntry.Text = string.Empty;

			var shellViewModel = this.Handler.MauiContext.Services.GetService<ShellViewModelWin>();
			shellViewModel.SelectedRoute = "account";
			shellViewModel.SelectedRoute = "chat";

			await Task.Delay(200);
			ChatCollectionView.ScrollTo(_viewModel.Messaggi.Count-1);
		}
	}
}