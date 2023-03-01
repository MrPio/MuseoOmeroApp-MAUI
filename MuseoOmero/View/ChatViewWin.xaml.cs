namespace MuseoOmero.ViewWin;

public partial class ChatViewWin : ContentPage
{
	ChatViewModelWin _viewModel;
	private ShellViewModelWin _shellViewModelWin;

	public ChatViewWin(ChatViewModelWin viewModel, ShellViewModelWin shellViewModelWin)
	{
		viewModel.CollectionViewCallback = ScrollChat;
		_viewModel = viewModel;
		BindingContext = _viewModel;
		_viewModel.Initialize();
		InitializeComponent();
		_shellViewModelWin = shellViewModelWin;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		//_shellViewModelWin.SelectedRoute = "blank";
		//_shellViewModelWin.SelectedRoute = "chat";
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

	private async Task ScrollChat()
	{
		await Task.Delay(400);
		ChatCollectionView.ScrollTo(_viewModel.Messaggi.Count - 1);
	}

	private async void HighlightView_Clicked(object sender, EventArgs e)
	{
		if (_viewModel.CurrentUtente is null)
			return;

		var text = SendEntry.Text;
		if (text.Length > 0)
		{
			var messaggio = new Messaggio(DateTime.Now, text);
			_viewModel.Messaggi.Add(new(messaggio, false));
			SendEntry.Text = string.Empty;

			// L'unica soluzione che ho trovato dopo moltissimi tentativi per 
			// trigger-are l'aggiornamento della collection view
			_shellViewModelWin.SelectedRoute = "blank";
			_shellViewModelWin.SelectedRoute = "chat";

			await _viewModel.SendMessage(messaggio);
			await ScrollChat();
		}
	}
}