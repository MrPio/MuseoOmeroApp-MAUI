namespace MuseoOmero.ViewModelWin;
public partial class HomeViewModelWin : ObservableObject
{
	[ObservableProperty]
	PanoramicaElementViewModelWin bigliettiPanoramica = new(
		icon: IconFont.Ticket,
		title: "Biglietti",
		dark: true
	);

	[ObservableProperty]
	PanoramicaElementViewModelWin questionariPanoramica = new(
		icon: IconFont.Account,
		title: "Questionari",
		dark: false
	);

	[ObservableProperty]
	PanoramicaElementViewModelWin chatPanoramica = new(
		icon: IconFont.Chat,
		title: "Chat",
		dark: false
	);

	[ObservableProperty]
	List<string> sale = new() { "Ingresso", "Greco e Romano", "Mimica del volto umano", "Ancona", "Medievale e '400", "Rinascimentale", "Dipinti", "'900 e Contemporaneo", "Movimento scolpito", "Deposito" };

	[ObservableProperty]
	List<string> mostre = new();

	public List<Opera> Opere = new();

	[ObservableProperty]
	List<Opera> opereFiltrate = new();

	public async void Initialize()
	{
		var db = DatabaseManager.Instance;

		Opere = await db.LoadJsonArray<Opera>("opere");//TODO filtrare e mostrare + NIENTE DA MOSTRARE

		var mostre = await db.LoadJsonArray<Mostra>("mostre");
		if(mostre is { })
		{
			this.Mostre = (from m in mostre select m.Titolo).ToList();
		}

		var users = await db.LoadJsonArray<Utente>("utenti");
		var bigliettiOggi = new List<Biglietto>();
		var bigliettiVendutiOggi = new List<Biglietto>();
		var convalideOggi = 0;
		var questionariOggi = new List<Questionario>();
		var compilazioniTotali = 0;
		var messaggiNonLetti = new List<Messaggio>();
		var chatTotali = 0;
		var chatNonLetteTotali = 0;

		foreach (var user in users)
		{
			bigliettiOggi.AddRange(from b in user.Biglietti
								   where b.DataValidita.Date == DateTime.Today
								   select b);
			bigliettiVendutiOggi.AddRange(from b in user.Biglietti
										  where b.DataAcquisto.Date == DateTime.Today
										  select b);
			questionariOggi.AddRange(from q in user.Questionari
									 where q.DataCompilazione.Date == DateTime.Today
									 select q);
			compilazioniTotali += user.Questionari.Count;

			if (user.Chat is { })
			{

				messaggiNonLetti.AddRange(from m in user.Chat?.MessaggiUtente
										  where !m.Letto
										  select m);
				++chatTotali;
				chatNonLetteTotali += user.Chat.MessaggiUtente.Any(m => !m.Letto) ? 1 : 0;
			}
		}

		//BIGLIETTI
		foreach (var biglietto in bigliettiOggi)
			if (biglietto.DataConvalida is { } && biglietto.DataConvalida?.Date == DateTime.Today)
				++convalideOggi;

		BigliettiPanoramica.Subtitle = $"Venduti oggi: {bigliettiVendutiOggi.Count}";
		BigliettiPanoramica.Content = $"Prenotati per oggi: {bigliettiOggi.Count}";
		BigliettiPanoramica.UnderContent = $"Convalidati oggi: {convalideOggi}";
		BigliettiPanoramica.TrendingIcon = convalideOggi > bigliettiOggi.Count / 2 ? IconFont.TrendingDown : IconFont.TrendingUp;

		//QUESTIONARI
		var percentualeCompilazioni = (float)compilazioniTotali / convalideOggi * 100f;
		QuestionariPanoramica.Subtitle = $"Totali: {compilazioniTotali}";
		QuestionariPanoramica.Content = $"Compilati oggi: {questionariOggi.Count}";
		QuestionariPanoramica.UnderContent = $"Tasso compilazione: {percentualeCompilazioni.ToString("0.00")}%";
		QuestionariPanoramica.TrendingIcon = percentualeCompilazioni < 50 ? IconFont.TrendingDown : IconFont.TrendingUp;

		//CHAT
		ChatPanoramica.Subtitle = $"Totali: {chatTotali}";
		ChatPanoramica.Content = $"Messaggi: {messaggiNonLetti.Count}";
		ChatPanoramica.UnderContent = $"Chat non lette: {chatNonLetteTotali}";
		ChatPanoramica.TrendingIcon = chatNonLetteTotali < 2 ? IconFont.TrendingDown : IconFont.TrendingUp;
	}
}