namespace MuseoOmero.Managers;
public class DbPopulatorManager
{
	private static DbPopulatorManager _instance;
	private DbPopulatorManager() { }
	public static DbPopulatorManager Instance
	{
		get
		{
			_instance ??= new DbPopulatorManager();
			return _instance;
		}
	}
	private DatabaseManager db { get => DatabaseManager.Instance; }

	public async Task populateUtenti()
	{
		var user = new Utente("MrPio", "a", "a", "a", "a", "a", new List<Biglietto>(), new List<Questionario>(), null);
		var user2 = new Utente("MrPio2", "a", "a", "a", "a", "a", new List<Biglietto>(), new List<Questionario>(), null);

		await db.Post($"utenti/", user);
		await db.Post($"utenti/", user2);

		var b = new Biglietto(
			dataAcquisto: DateTime.Now,
			dataValidita: DateTime.Today.AddDays(1),
			tipologia: TipoBiglietto.MuseoAperto,
			dataGuida: null
		);
		user.Biglietti.AddRange(new List<Biglietto>() { b, b });
		await db.SaveAccount(user);
		user2.Biglietti.AddRange(new List<Biglietto>() { b, b, b, b });
		await db.SaveAccount(user2);
	}

	public async Task populateOpere()
	{
		var opere = new List<Opera>()
		{
			new(
				sala:"Ingresso",
				nome:"Ingranaggio",
				autore:"Umberto Mastroianni",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{29f,20f,9.5f },
				tecnica:new(){ "fusione","doratura","lucidatura"},
				materiali:new(){"bronzo"},
				foto:"https://www.museoomero.it/wp-content/uploads/2020/06/opera-Mastroianni-Ingranaggio-2010-1502A-1024x1536-1.jpg",
				descrizione:"“Oggi la riapparizione di una\r\nforma è quasi leggenda.\r\n\r\nOggi la rinascita di una\r\nforma può\r\nriportare al vertice il\r\ndivenire di un pensiero\r\npuro originale\r\narrivato sulla terra stanca”.\r\nUmberto Mastroianni\r\n\r\n“Ingranaggio”, opera inserita all’interno della sezione itinerante “Bello e Accessibile” del Museo Omero, è una piccola scultura in bronzo (alta 20 centimetri) realizzata da Umberto Mastroianni nel 1988.\r\n\r\nInnestata su una base in marmo bianco a forma di parallelepipedo, la scultura è un assemblaggio di forme astratte costruite su linee curve e spezzate. Dalla base si innalzano forme dallo slancio verticale, con curve tondeggianti e sporgenti sul profilo sinistro, e rientranti e spigolose sullo quello destro. Forme che accompagnano l’occhio e la mano verso due cerchi, scavati all’interno, uno a destra uno a sinistra. Da questi cerchi, che ricordano due “anelli”, si aprono sulla sommità due “corone dentate”. Sopra l’anello di sinistra, più grande (4 centimetri di diametro) si apre la corona con cinque denti di varie dimensioni, simili ai merli delle architetture militari medievali, sopra l’anello di destra (3 centimetri di diametro) si apre la corona più piccola con tre denti.\r\nLa superficie del bronzo è lucida e levigata, liscia e piacevole al tatto. Sul retro, lungo la forma verticale che sale dal basamento della scultura, è incisa la firma dell’artista.\r\n\r\nUn insieme di forme che potrebbe suggerire la visione di un essere zoomorfo, un animale astratto, con becco aperto, cresta e occhio. Sicuramente l’effetto generale è di grande dinamismo e, come suggerisce il titolo, ci riporta alla mente un ingranaggio, un meccanismo in movimento. L’opera è parte di una lunga serie di studi sulla macchina e il suo dinamismo, che l’artista pone al centro della propria ricerca artistica ed estetica.\r\n“Ingranaggio” si inserisce nella penultima fase creativa di Mastroianni: dopo gli anni 60/70, periodo delle esplosioni violente, dalla plasticità dirompente, nelle sue creazioni appaiono ingranaggi, meccanismi integrati a forme geometriche inedite, non prive di senso ironico, che lo porta di frequente a suggerire l’esistenza di un fantastico mondo di animali meccanicizzati.",
				visualizzazioni:4
				),

			new(
				sala:"Greco e Romano",
				nome:"Partenone",
				autore:"Gruppo Ricalchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 113f, 240f, 100f },
				tecnica:new(){ "assemblaggio"},
				materiali:new(){"legno", "gesso", "resina"},
				foto:"https://www.museoomero.it/wp-content/uploads/2020/07/opera-Fidia-partenone-2010-3008.64-1.jpg",
				descrizione:"“Il Partenone racchiude un’armonica sintesi di utilità, solidità e piacevolezza”, Vitruvio da “De Architectura”.\r\n\r\nIl modellino dettagliato del Partenone, lungo circa 2 metri e mezzo e realizzato in legno, gesso e resina, ricostruisce il tempio come doveva apprezzarsi alle sue origini, nella sua completezza. Progettato da Fidia è considerato “il tempio perfetto”. Di forma rettangolare, in marmo pentelico, il tempio si innalza su tre gradoni, ed è circondato da colonne di ordine dorico: 8 sul lato corto (37 metri) e 17 su quello lungo (79 metri). Casa della dea Atena, protettrice di Atene, il tempio è collocato in cima all’acropoli, la parte più alta della città greca.\r\n\r\nIl modellino, dotato di ruote, può essere aperto in due parti per consentire una migliore esplorazione degli interni. La divisione è all’incirca dopo la metà, tra la cella, ovvero il luogo sacro dove era conservata la statua sacra delle Dea (naos), e la stanza dietro la cella (opistodomo) dove si trovavano il tesoro e gli arredi sacri. Due aperture sul tetto permettono di toccare sia il colonnato interno, che circondava la cella, sia la ricostruzione della statua della dea Atena, eseguita da Fidia e andata perduta. Sulla parete esterna della cella un fregio continuo, una decorazione con andamento orizzontale, in stile ionico, narra in rilievo la processione Panatenaica, una processione che si svolgeva durante le festività in onore di Atena. La statua di Atena è scolpita in piedi, con un elmo, una corazza pettorale, la lancia e lo scudo: i suoi attributi tipici.\r\n\r\nTornando al perimetro esterno è percepibile, sopra il colonnato, una trabeazione con un fregio dorico: una decorazione che alterna piccoli rettangoli con altorilievi (metope) a scanalature verticali (triglifi). Sopra la trabeazione i due frontoni sono ornati di statue, anche se poco percepibili al tatto: il lato est mostra la nascita di Atena dal capo di Zeus; il lato ovest narra la sfida tra Atena e Poseidone per il dominio dell’Attica.\r\n\r\nIl Partenone fu commissionato da Pericle nel 445 a.C. e realizzato dagli architetti Ictino e Callicrate, supervisionati da Fidia. Il nome Partenone deriva dall’attributo di Atena “parthenos”, cioè “vergine”. Durante i secoli venne trasformato in chiesa e nel 1456 in moschea, mentre nel 1687 fu parzialmente distrutto un colpo di cannone dei Veneziani, in guerra contro l’Impero Ottomano. Molti marmi furono inoltre asportati dall’ambasciatore britannico Lord Elgin nella prima metà dell’800 e sono custoditi al British Museum.\r\n\r\nNonostante i danni subiti, il Partenone ha mantenuto intatto il suo fascino e la sua fama; tuttora, dall’acropoli veglia sulla città di Atene di cui è un simbolo indiscusso.",
				visualizzazioni:12
				),

						new(
				sala:"Greco e Romano",
				nome:"Discobolo",
				autore:"Gruppo Ricalchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 156f, 108f, 100f },
				tecnica:new(){ "calco al vero"},
				materiali:new(){"gesso alabastrino"},
				foto:"https://www.museoomero.it/wp-content/uploads/2020/07/opera-Discobolo-1304-0301B-1.jpg",
				descrizione:"“L’antichità poteva dire di lui che aveva moltiplicato la verità ma che, curioso soprattutto dei corpi, non aveva reso lo stato dell’animo. E così Mirone, mentre è il primo dei grandi scultori dell’antichità, è l’ultimo degli scultori arcaici”, Alessandro della Seta.\r\n\r\nIl “Discobolo” è una scultura realizzata in bronzo da Mirone nel 455 avanti Cristo. Presso il Museo Omero è esposta una copia da calco al vero in gesso di una copia romana in marmo ritrovata nella Villa Adriana di Tivoli.\r\n\r\nLa statua rappresenta un atleta a dimensioni naturali, alto circa 160 centimetri, che sta per lanciare il disco.\r\nIl viso e il busto sono chinati in avanti; il braccio destro, la cui mano regge il disco, è portato indietro per prendere lo slancio, mentre il sinistro forma un semiarco in avanti, col polso che arriva a sfiorare il ginocchio destro. Le ginocchia sono flesse, la gamba destra è leggermente più avanti dell’altra e pare sostenere il peso del corpo, mentre il piede sinistro poggia solo sulla punta.\r\n\r\nUno dei punti di forza dell’opera è l’armonia della composizione: facendo scorrere le dita, dalla mano che stringe il disco fino al tallone del piede sinistro, ci si accorge che le linee delle varie parti del corpo disegnano un arco.\r\nMirone ha posto grande cura nella resa di alcuni dettagli anatomici, come la muscolatura del torace, i tendini e le vene della mano destra che si gonfiano per lo sforzo. Lo scultore rappresenta il corpo nel momento della massima tensione, che però non si riflette nel volto idealizzato: questo esprime solo una tenue concentrazione, tradita dalla bocca semiaperta e da una leggera ruga sulla fronte.\r\nDietro la figura è presente un fusto d’albero, che sicuramente nell’originale in bronzo era assente, ma che è stato necessario aggiungere nella copia romana in marmo, con funzione puramente statica.\r\n\r\nIl Discobolo è sicuramente l’opera più nota di Mirone nonché un’icona dell’arte greca. In essa si concentrano alcune delle maggiori suggestioni legate all’antica Grecia: la passione per i giochi olimpici, il culto della perfezione del corpo umano, la calma interiore e la ricerca dell’armonia formale. Il bronzo forse fu realizzato per la città di Sparta e poteva rappresentare Giacinto, il ragazzo amato da Apollo.",
				visualizzazioni:12
				),
		};
		foreach (var opera in opere)
			await db.Post("opere", opera);
	}
}
