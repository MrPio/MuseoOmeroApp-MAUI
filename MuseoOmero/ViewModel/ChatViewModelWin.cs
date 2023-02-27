namespace MuseoOmero.ViewModelWin;

public partial class ChatViewModelWin : ObservableObject
{
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
	public ChatViewModelWin(HomeViewModelWin homeViewModelWin)
	{
		HomeViewModel = homeViewModelWin;
	}

	[RelayCommand]
	void SelectChat(Chat chat)
	{
		List<MessaggioConMittente> messaggi = new();
		messaggi.AddRange(from m in chat.MessaggiUtente select new MessaggioConMittente(m,true));
		messaggi.AddRange(from m in chat.MessaggiMuseo select new MessaggioConMittente(m, false));
		messaggi = messaggi.OrderBy(m => m.Messaggio.Data).ToList();

		Messaggi.Clear();
		foreach (var m in messaggi)
			Messaggi.Add(m);
		//Messaggi = new(messaggi);
	}

	public void Initialize()
	{
		UtentiConChat = new(HomeViewModel.Utenti.FindAll(u => u.Chat is { }));
		NoChats = UtentiConChat.Count == 0;
		foreach (var utente in UtentiConChat)
			Chats.Add(utente.Chat);
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
		Messaggio = messaggio;
		DiUtente = diUtente;
	}
}
