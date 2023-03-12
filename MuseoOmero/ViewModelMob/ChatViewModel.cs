namespace MuseoOmero.ViewModelMob;
public partial class ChatViewModel : ObservableObject
{
	private IDisposable _chatObserver, _miaChatObserver;
	public Action CollectionViewCallback = null;

	[ObservableProperty]
	ObservableCollection<MessaggioConMittente> messaggi = new();

	[ObservableProperty]
	bool noMessaggi = true, isBusy;

	[ObservableProperty]
	string filtro, emptyViewTitle, emptyView;
	partial void OnFiltroChanged(string value) => Initialize();

	public void SendMessage(Messaggio messaggio)
	{
		var utente = AccountManager.Instance.Utente;
		utente.Chat ??= new(new(), new(), DateTime.Now);
		NoMessaggi = false;

		utente.Chat.MessaggiUtente.Add(messaggio);
		_ = DatabaseManager.Instance.Put($"utenti/{utente.Uid}/chat/messaggi_utente/{utente.Chat.MessaggiUtente.Count - 1}", messaggio);
	}
	public void DisposeObservers()
	{
		_chatObserver?.Dispose();
		_miaChatObserver?.Dispose();
	}
	public void Initialize()
	{
		var utente = AccountManager.Instance.Utente;
		var chat = utente.Chat;

		// popolo i messaggi
		{
			if (chat is null)
			{
				EmptyViewTitle = "Nessun Messaggio";
				EmptyView = "Non ci hai ancora contattati, utilizza questa chat per in qualsiasi dubbio!";
				NoMessaggi = true;
				return;
			}
			List<MessaggioConMittente> messaggi = new();
			messaggi.AddRange(from m in chat.MessaggiUtente where m is { } select new MessaggioConMittente(m.Clone(), true));
			messaggi.AddRange(from m in chat.MessaggiMuseo where m is { } select new MessaggioConMittente(m.Clone(), false));
			bool filtrati = false;
			if (Filtro is { } && Filtro.Length > 2)
			{
				EmptyViewTitle = $"Ricerca di \"{Filtro}\"";
				EmptyView = "Nessun risultato";
				messaggi = messaggi.Where(m => m.Messaggio.Testo.ToLower().Contains(Filtro.ToLower())).ToList();
				foreach (var m in messaggi)
				{
					var testo = m.Messaggio.Testo.ToLower();
					var filtro = Filtro.ToLower();
					var index = testo.IndexOf(filtro);
					m.Messaggio.Testo = m.Messaggio.Testo.Replace(
						m.Messaggio.Testo[index..(index + filtro.Length)],
						$"**{filtro}**");
					filtrati = true;
				}
			}
			messaggi = messaggi.OrderBy(m => m.Messaggio.Data).ToList();
			if (Messaggi.Count != messaggi.Count || filtrati)
				Messaggi = new(messaggi);

			NoMessaggi = Messaggi.Count == 0;
		}

		// initializzo i 2 observer(s)
		{
			_chatObserver ??= DatabaseManager.Instance.Observe<Messaggio>(
					resource: $"utenti/{utente.Uid}/chat/messaggi_museo",
					callback: (o) =>
					{
						var msg = o.Object;
						if (msg is null)
							return;
						// Se il museo ha modificato il testo di un messaggio che già è nella collection view
						if (Messaggi.ToList().Find(m => m.Messaggio.Data == msg.Data) is { } m)
							m.Messaggio = msg;
						else
						{
							Messaggi.Add(new(msg, false));
							utente.Chat.MessaggiMuseo.Add(msg);

							//segno il messaggio ricevuto come letto
							if (!msg.Letto)
							{
								msg.Letto = true;
								DatabaseManager.Instance.Put($"utenti/{utente.Uid}/chat/messaggi_museo/{utente.Chat.MessaggiMuseo.Count - 1}/letto", true);
							}
							Task.Delay(500).ContinueWith(_ => MainThread.BeginInvokeOnMainThread(() => CollectionViewCallback()));
						}
					}
				);
			_miaChatObserver ??= DatabaseManager.Instance.Observe<Messaggio>(
					resource: $"utenti/{utente.Uid}/chat/messaggi_utente",
					callback: (o) =>
					{
						var msg = o.Object;
						if (msg is null)
							return;
						if (Messaggi.ToList().Find(m => m.Messaggio.Data == msg.Data) is { } m)
							Messaggi[Messaggi.IndexOf(m)] = new(msg, true);
						else
						{
							Messaggi.Add(new(msg, true));
							Task.Delay(500).ContinueWith(_ => MainThread.BeginInvokeOnMainThread(() => CollectionViewCallback()));
						}

					}
				);
		}

		// segno come letti i messaggi ricevuti
		{
			var changes = false;
			foreach (var m in chat.MessaggiMuseo.Where(m => m is { } && !m.Letto))
				m.Letto = changes = true;
			if (changes)
				DatabaseManager.Instance.Put($"utenti/{utente.Uid}/chat/", utente.Chat);
		}

		if (CollectionViewCallback is { })
			Task.Delay(400).ContinueWith((_) => CollectionViewCallback.Invoke());
	}
}