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

	private void ScrollChat()
	{
		ChatCollectionView.ScrollTo(Math.Max(0,_viewModel.Messaggi.Count + 2));
	}

	private void HighlightView_Clicked(object sender, EventArgs e)
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
			//SOLUZIONE: mettere l'await come ultima istruzione nel viewModel

			//_shellViewModelWin.SelectedRoute = "blank";
			//_shellViewModelWin.SelectedRoute = "chat";

			_viewModel.SendMessage(messaggio);
			ScrollChat();
		}
	}
}