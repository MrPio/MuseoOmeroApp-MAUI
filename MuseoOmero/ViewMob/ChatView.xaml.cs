namespace MuseoOmero.ViewMob;

public partial class ChatView : ContentView
{
	ChatViewModel _viewModel;
	MainViewModel _mainViewModel;
	bool wavesRestricted;
	double lastOffset;
	public ChatView()
	{
		_viewModel = Service.Get<ChatViewModel>();
		_mainViewModel = Service.Get<MainViewModel>();
		InitializeComponent();

		_viewModel.CollectionViewCallback = ScrollChat;
		_viewModel.Initialize();
	}


	private void ScrollChat()
	{
		ChatCollectionView.ScrollTo(_viewModel.Messaggi.Count + 3);
	}
	private void Send_Clicked(object sender, EventArgs e)
	{
		var text = SendEntry.Text;
		if (text.Length > 0)
		{
			var messaggio = new Messaggio(DateTime.Now, text);
			_viewModel.Messaggi.Add(new(messaggio, true));
			SendEntry.Text = string.Empty;

			_viewModel.SendMessage(messaggio);
			Task.Delay(500).ContinueWith(_ => ScrollChat());
		}
	}

	private void ChatCollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
	{
		var offset = e.VerticalOffset;

		if (offset < lastOffset-50 && !wavesRestricted || offset > lastOffset+50 && wavesRestricted)
		{
			var anim = new[]{
				new[] { 0, 2 } ,
				new[] { 50, 10 } ,
			};
			if (wavesRestricted)
			{
				anim[0] = anim[0].Reverse().ToArray();
				anim[1] = anim[1].Reverse().ToArray();
			}

			if (this.AnimationIsRunning("waves"))
				return;
			this.Animate("waves", new Animation(v => _mainViewModel.WavesExpandFactor = v, anim[0][0], anim[0][1], easing: Easing.CubicOut), length: 600);
			this.Animate("mainGrid", new Animation(v => MainGrid.Padding=new(10,v), anim[1][0], anim[1][1], easing: Easing.CubicOut), length: 400);
			wavesRestricted.Swap();
			lastOffset = offset;
		}
		if(offset > lastOffset && !wavesRestricted|| offset < lastOffset && wavesRestricted)
			lastOffset = offset;
	}
}