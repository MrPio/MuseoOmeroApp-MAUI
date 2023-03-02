namespace MuseoOmero.ViewModelWin;

public partial class ChatViewModelWin : ObservableObject
{
	private ShellViewModelWin _shellViewModelWin;
	private IDisposable _chatObserver, _miaChatObserver;
	public Action CollectionViewCallback = null;
	[ObservableProperty]
	HomeViewModelWin homeViewModel;

	[ObservableProperty]
	ObservableCollection<Utente> utentiConChat = new();

	[ObservableProperty]
	ObservableCollection<Chat> chats = new();

	[ObservableProperty]
	ObservableCollection<MessaggioConMittente> messaggi = new();

	[ObservableProperty]
	bool noChats = true;
	public Utente CurrentUtente;
	public ChatViewModelWin(HomeViewModelWin homeViewModelWin, ShellViewModelWin shellViewModelWin)
	{
		HomeViewModel = homeViewModelWin;
		_shellViewModelWin = shellViewModelWin;
	}

	[ObservableProperty]
	bool isBusy;

	[RelayCommand]
	async void SelectChat(Chat chat)
	{
		List<MessaggioConMittente> messaggi = new();
		messaggi.AddRange(from m in chat.MessaggiUtente where m is { } select new MessaggioConMittente(m, true));
		messaggi.AddRange(from m in chat.MessaggiMuseo where m is { } select new MessaggioConMittente(m, false));
		messaggi = messaggi.OrderBy(m => m.Messaggio.Data).ToList();

		Messaggi = new(messaggi);

		_shellViewModelWin.SelectedRoute = "blank";
		_shellViewModelWin.SelectedRoute = "chat";

		if (CollectionViewCallback is { })
		{
			await Task.Delay(400);
			CollectionViewCallback.Invoke();
		}

		if (_chatObserver is { })
			_chatObserver.Dispose();
		CurrentUtente = UtentiConChat.ToList().Find(u => u.Chat == chat);

		_chatObserver = DatabaseManager.Instance.Observe<Messaggio>(
			resource: $"utenti/{CurrentUtente.Uid}/chat/messaggi_utente",
			callback: (o) =>
			{
				var msg = o.Object;
				if (o.Object is null)
					return;
				// Se l'utente ha modificato il testo di un messaggio che già è nella collection view
				if (Messaggi.ToList().Find(m => m.Messaggio.Data == msg.Data) is { } m)
					m.Messaggio = msg;
				else
					Messaggi.Add(new(msg, true));
				//CollectionViewCallback.Invoke();

			}
		);

		if (_miaChatObserver is { })
			_miaChatObserver.Dispose();

		_miaChatObserver = DatabaseManager.Instance.Observe<Messaggio>(
	resource: $"utenti/{CurrentUtente.Uid}/chat/messaggi_museo",
	callback: (o) =>
	{
		//HomeViewModel.LoadUtenti();
		var msg = o.Object;
		if (o.Object is null)
			return;
		if (Messaggi.ToList().Find(m => m.Messaggio.Data == msg.Data) is { } m)
			m.Messaggio = msg;
		else
			Messaggi.Add(new(msg, true));

		//CollectionViewCallback.Invoke();
	}
);


		// segno come letti i messaggi ricevuti
		var changes = false;
		foreach (var m in chat.MessaggiUtente.Where(m => !m.Letto))
			m.Letto = changes = true;
		if (changes)
			DatabaseManager.Instance.Put($"utenti/{CurrentUtente.Uid}/chat/", CurrentUtente.Chat);
	}

	public async Task LoadAvatars()
	{
		var utenti = new List<Utente>();

		foreach (var utente in UtentiConChat)
		{
			var url = await StorageManager.Instance.GetLink($"{utente.Uid}/foto_profilo/");
			utente.FotoProfilo = url is { } ? url : ImagesOnline.Anonymous;
			Chats.Add(utente.Chat);
			utenti.Add(utente);
		}
		UtentiConChat = new(utenti);

	}

	public void Initialize()
	{
		IsBusy = true;
		var utenti = HomeViewModel.Utenti.FindAll(u => u.Chat is { });
		NoChats = utenti.Count() == 0;
		UtentiConChat = new(utenti);
		LoadAvatars();
		IsBusy = false;
	}

	public void SendMessage(Messaggio messaggio)
	{
		CurrentUtente.Chat.MessaggiMuseo.Add(messaggio);
		_ = DatabaseManager.Instance.Put($"utenti/{CurrentUtente.Uid}/chat/messaggi_museo/{CurrentUtente.Chat.MessaggiMuseo.Count - 1}", messaggio);
	}
}

public partial class MessaggioConMittente : ObservableObject
{

	[ObservableProperty]
	Messaggio messaggio;

	[ObservableProperty]
	bool diUtente;
	public MessaggioConMittente(Messaggio messaggio, bool diUtente)
	{
		this.Messaggio = messaggio;
		DiUtente = diUtente;
	}
}
