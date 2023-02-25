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
		var user = new Utente("MrPio", "Valerio", "Morelli", "+39 3318162818", new List<Biglietto>(), new List<Questionario>(), null);
		await db.Put($"utenti/JTOjcHsfYIY1SqmDQbw7kLalnYw2", user);

		var b = new Biglietto(
			dataAcquisto: DateTime.Now,
			dataValidita: DateTime.Today.AddDays(1),
			tipologia: TipoBiglietto.MuseoAperto,
			dataGuida: null
		);
		user.Biglietti.AddRange(new List<Biglietto>() { b, b });
		await db.Put($"utenti/JTOjcHsfYIY1SqmDQbw7kLalnYw2/biglietti", user.Biglietti);
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
				foto:"https://i.ibb.co/fGrdTHY/opera-Mastroianni-Ingranaggio-2010-1502-A-1024x1536-1.jpg",
				descrizione:"“Oggi la riapparizione di una\r\nforma è quasi leggenda.\r\n\r\nOggi la rinascita di una\r\nforma può\r\nriportare al vertice il\r\ndivenire di un pensiero\r\npuro originale\r\narrivato sulla terra stanca”.\r\nUmberto Mastroianni\r\n\r\n“Ingranaggio”, opera inserita all’interno della sezione itinerante “Bello e Accessibile” del Museo Omero, è una piccola scultura in bronzo (alta 20 centimetri) realizzata da Umberto Mastroianni nel 1988.\r\n\r\nInnestata su una base in marmo bianco a forma di parallelepipedo, la scultura è un assemblaggio di forme astratte costruite su linee curve e spezzate. Dalla base si innalzano forme dallo slancio verticale, con curve tondeggianti e sporgenti sul profilo sinistro, e rientranti e spigolose sullo quello destro. Forme che accompagnano l’occhio e la mano verso due cerchi, scavati all’interno, uno a destra uno a sinistra. Da questi cerchi, che ricordano due “anelli”, si aprono sulla sommità due “corone dentate”. Sopra l’anello di sinistra, più grande (4 centimetri di diametro) si apre la corona con cinque denti di varie dimensioni, simili ai merli delle architetture militari medievali, sopra l’anello di destra (3 centimetri di diametro) si apre la corona più piccola con tre denti.\r\nLa superficie del bronzo è lucida e levigata, liscia e piacevole al tatto. Sul retro, lungo la forma verticale che sale dal basamento della scultura, è incisa la firma dell’artista.\r\n\r\nUn insieme di forme che potrebbe suggerire la visione di un essere zoomorfo, un animale astratto, con becco aperto, cresta e occhio. Sicuramente l’effetto generale è di grande dinamismo e, come suggerisce il titolo, ci riporta alla mente un ingranaggio, un meccanismo in movimento. L’opera è parte di una lunga serie di studi sulla macchina e il suo dinamismo, che l’artista pone al centro della propria ricerca artistica ed estetica.\r\n“Ingranaggio” si inserisce nella penultima fase creativa di Mastroianni: dopo gli anni 60/70, periodo delle esplosioni violente, dalla plasticità dirompente, nelle sue creazioni appaiono ingranaggi, meccanismi integrati a forme geometriche inedite, non prive di senso ironico, che lo porta di frequente a suggerire l’esistenza di un fantastico mondo di animali meccanicizzati.",
				visualizzazioni:4
				),

			new(
				sala:"Ingresso",
				nome:"Patura umana",
				autore:"Alessia Crema",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{50,27,13f },
				tecnica:new(){ "modellazione a mano libera","pittura"},
				materiali:new(){"terracotta","legno"},
				foto:"https://i.ibb.co/3rVwrkL/opera-la-paura-umana-2010-1501-A-1024x1536-1.jpg",
				descrizione:"“Possiamo avere religioni diverse, lingue diverse, colori della pelle diversa, ma apparteniamo tutti alla stessa razza umana”, Kofi Annan.\r\n\r\n“Natura Umana” è una scultura in terracotta di Alessia Crema, vincitrice della Biennale Arteinsieme 2019, ed entrata a far parte della collezione del Museo Omero.\r\n\r\nQuesta scultura alta 50 centimetri, rappresenta due figure a mezzo busto, affiancate, che sembrano fondersi in un’unica entità: condividono infatti un occhio e un orecchio nel punto di contatto, mentre il naso, la bocca, l’occhio e l’orecchio esterni rimangono separati. Le figure, una bianca e una nera, sono calve e attraversate da fessure orizzontali ondulate dove si uniscono. Ai piedi dei profili troviamo ammucchiate una serie di lettere in stampatello maiuscolo realizzate in legno e poi colorate d’oro. Tattilmente la scultura si presenta movimentata e leggermente ruvida.\r\n\r\nLa giovane autrice, Alessia Crema, rappresenta le differenze più evidenti tra gli uomini: le apparenze, il colore della pelle, la lingua, la cultura. La domanda è “Queste differenze ci allontanano l’uno dall’altro?”. La risposta è in queste due figure che nascono dallo stesso soggetto e che in esso si riuniscono. Siamo tutti uguali nella nostra natura di uomini e le differenze sono da considerare una fonte di ricchezza. L’opera, come da concorso, è ispirata alla poetica della scultrice Rabarama, testimonial dell’ottava edizione della Biennale (2019).",
				visualizzazioni:4
				),

			new(
				sala:"Ingresso",
				nome:"Mole Vanvitelliana, modello volumetrico",
				autore:"Fabio Ridolfi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{10,87,81f },
				tecnica:new(){ "incisione","intaglio"},
				materiali:new(){"legno"},
				foto:"https://i.ibb.co/YNPx5Cy/opera-vanvitelli-Mole-Vanvitelliana-1.jpg",
				descrizione:"“Estremamente laborioso, e disegnatore indefesso, egli riuniva qualità sovente discordi, prontezza d’ingegno e sofferenza di studio, vivacità di spirito e ostinazione di fatica. In mezzo a tante occupazioni e gloria sì rara, era sempre umano, moderato, piacevole, discreto cogli operai, pietoso con i miseri, cortese con tutti. […] Raro ed imitabile esempio di lodevolissima onestà, [Vanvitelli era] di dolci costumi, nettissimo d’invidia, affabile e sincero per natura era da tutti desiderato, ed amici aveva moltissimi” Luigi Vanvitelli junior (nipote dell’architetto), “Vita dell’architetto Luigi Vanvitelli”.\r\n\r\nAll’ingresso del percorso espositivo è collocato il modello volumetrico della Mole Vanvitelliana di Ancona, l’imponente struttura che ospita il Museo Omero. Conosciuta anche come Ex Lazzeretto, fu ideata Luigi Vanvitelli e costruita tra il 1733 e il 1743. Il modellino volumetrico, realizzato da Fabio Ridolfi, è in legno e riproduce in scala 1:250 i volumi essenziali dell’edificio così come era in origine: una grande architettura di forma pentagonale poggiante su un’isola artificiale di 20.000 metri quadrati, e circondata dall’acqua, senza collegamenti con la terraferma.\r\n\r\nLa parte più esterna della Mole Vanvitelliana consiste in una cinta muraria dotata di due accessi e rinforzata da un rivellino (fortificazione) dal lato rivolto verso il mare aperto. Al suo interno gli edifici che compongono il corpo principale disegnano un secondo pentagono, che si sviluppa in verticale e lascia aperto al centro un cortile interno. Un tempietto in stile neoclassico di forma circolare è collocato al centro della corte. Le parti corrispondenti agli edifici e al cortile interno sono in legno di tiglio, mentre il tempietto è stato realizzato usando il cirmolo. Situato all’ingresso della collezione, il modello permette al visitatore non vedente di comprendere le forme del luogo in cui si trova.\r\n\r\nLa realizzazione del Lazzaretto della città rientrava nel progetto di ristrutturazione della città voluto da Papa Clemente XII, per dare alla città un’identità più moderna. Concepita inizialmente con funzione sanitaria (poteva contenere fino a 2000 persone), nel tempo la Mole ha assunto diverse destinazioni d’uso: da difesa bellica, a base navale, da magazzino, a raffineria e manifattura tabacchi. La sua funzione originaria è richiamata dal tempietto dedicato a San Rocco, protettore dei malati di peste. Oltre a servire come luogo di riunione in occasione delle cerimonie religiose nel lazzaretto, il tempietto, in marmo bianco, celava anche un complesso sistema di approvvigionamento di acqua potabile.\r\n\r\nIl Museo Omero, trasferitosi alla Mole nel 2012, occupa attualmente uno dei lati del pentagono in tutta la sua lunghezza.",
				visualizzazioni:4
				),

			//--------- GRECO-ROMANO ------------------------------------------------------

			new(
				sala:"Greco e Romano",
				nome:"Partenone",
				autore:"Gruppo Ricalchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 113f, 240f, 100f },
				tecnica:new(){ "assemblaggio"},
				materiali:new(){"legno", "gesso", "resina"},
				foto:"https://i.ibb.co/g60D2Jk/opera-Fidia-partenone-2010-3008-64-1.jpg",
				descrizione:"“Il Partenone racchiude un’armonica sintesi di utilità, solidità e piacevolezza”, Vitruvio da “De Architectura”.\r\n\r\nIl modellino dettagliato del Partenone, lungo circa 2 metri e mezzo e realizzato in legno, gesso e resina, ricostruisce il tempio come doveva apprezzarsi alle sue origini, nella sua completezza. Progettato da Fidia è considerato “il tempio perfetto”. Di forma rettangolare, in marmo pentelico, il tempio si innalza su tre gradoni, ed è circondato da colonne di ordine dorico: 8 sul lato corto (37 metri) e 17 su quello lungo (79 metri). Casa della dea Atena, protettrice di Atene, il tempio è collocato in cima all’acropoli, la parte più alta della città greca.\r\n\r\nIl modellino, dotato di ruote, può essere aperto in due parti per consentire una migliore esplorazione degli interni. La divisione è all’incirca dopo la metà, tra la cella, ovvero il luogo sacro dove era conservata la statua sacra delle Dea (naos), e la stanza dietro la cella (opistodomo) dove si trovavano il tesoro e gli arredi sacri. Due aperture sul tetto permettono di toccare sia il colonnato interno, che circondava la cella, sia la ricostruzione della statua della dea Atena, eseguita da Fidia e andata perduta. Sulla parete esterna della cella un fregio continuo, una decorazione con andamento orizzontale, in stile ionico, narra in rilievo la processione Panatenaica, una processione che si svolgeva durante le festività in onore di Atena. La statua di Atena è scolpita in piedi, con un elmo, una corazza pettorale, la lancia e lo scudo: i suoi attributi tipici.\r\n\r\nTornando al perimetro esterno è percepibile, sopra il colonnato, una trabeazione con un fregio dorico: una decorazione che alterna piccoli rettangoli con altorilievi (metope) a scanalature verticali (triglifi). Sopra la trabeazione i due frontoni sono ornati di statue, anche se poco percepibili al tatto: il lato est mostra la nascita di Atena dal capo di Zeus; il lato ovest narra la sfida tra Atena e Poseidone per il dominio dell’Attica.\r\n\r\nIl Partenone fu commissionato da Pericle nel 445 a.C. e realizzato dagli architetti Ictino e Callicrate, supervisionati da Fidia. Il nome Partenone deriva dall’attributo di Atena “parthenos”, cioè “vergine”. Durante i secoli venne trasformato in chiesa e nel 1456 in moschea, mentre nel 1687 fu parzialmente distrutto un colpo di cannone dei Veneziani, in guerra contro l’Impero Ottomano. Molti marmi furono inoltre asportati dall’ambasciatore britannico Lord Elgin nella prima metà dell’800 e sono custoditi al British Museum.\r\n\r\nNonostante i danni subiti, il Partenone ha mantenuto intatto il suo fascino e la sua fama; tuttora, dall’acropoli veglia sulla città di Atene di cui è un simbolo indiscusso.",
				visualizzazioni:11
				),

						new(
				sala:"Greco e Romano",
				nome:"Discobolo",
				autore:"Gruppo Ricalchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 156f, 108f, 100f },
				tecnica:new(){ "calco al vero"},
				materiali:new(){"gesso alabastrino"},
				foto:"https://i.ibb.co/y4BbkkF/opera-Discobolo-1304-0301-B-1.jpg",
				descrizione:"“L’antichità poteva dire di lui che aveva moltiplicato la verità ma che, curioso soprattutto dei corpi, non aveva reso lo stato dell’animo. E così Mirone, mentre è il primo dei grandi scultori dell’antichità, è l’ultimo degli scultori arcaici”, Alessandro della Seta.\r\n\r\nIl “Discobolo” è una scultura realizzata in bronzo da Mirone nel 455 avanti Cristo. Presso il Museo Omero è esposta una copia da calco al vero in gesso di una copia romana in marmo ritrovata nella Villa Adriana di Tivoli.\r\n\r\nLa statua rappresenta un atleta a dimensioni naturali, alto circa 160 centimetri, che sta per lanciare il disco.\r\nIl viso e il busto sono chinati in avanti; il braccio destro, la cui mano regge il disco, è portato indietro per prendere lo slancio, mentre il sinistro forma un semiarco in avanti, col polso che arriva a sfiorare il ginocchio destro. Le ginocchia sono flesse, la gamba destra è leggermente più avanti dell’altra e pare sostenere il peso del corpo, mentre il piede sinistro poggia solo sulla punta.\r\n\r\nUno dei punti di forza dell’opera è l’armonia della composizione: facendo scorrere le dita, dalla mano che stringe il disco fino al tallone del piede sinistro, ci si accorge che le linee delle varie parti del corpo disegnano un arco.\r\nMirone ha posto grande cura nella resa di alcuni dettagli anatomici, come la muscolatura del torace, i tendini e le vene della mano destra che si gonfiano per lo sforzo. Lo scultore rappresenta il corpo nel momento della massima tensione, che però non si riflette nel volto idealizzato: questo esprime solo una tenue concentrazione, tradita dalla bocca semiaperta e da una leggera ruga sulla fronte.\r\nDietro la figura è presente un fusto d’albero, che sicuramente nell’originale in bronzo era assente, ma che è stato necessario aggiungere nella copia romana in marmo, con funzione puramente statica.\r\n\r\nIl Discobolo è sicuramente l’opera più nota di Mirone nonché un’icona dell’arte greca. In essa si concentrano alcune delle maggiori suggestioni legate all’antica Grecia: la passione per i giochi olimpici, il culto della perfezione del corpo umano, la calma interiore e la ricerca dell’armonia formale. Il bronzo forse fu realizzato per la città di Sparta e poteva rappresentare Giacinto, il ragazzo amato da Apollo.",
				visualizzazioni:12
				),
						new(
				sala:"Greco e Romano",
				nome:"Auriga di Delfi",
				autore:"Gruppo Ricalchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 156f, 108f, 100f },
				tecnica:new(){ "calco al vero"},
				materiali:new(){"gesso alabastrino"},
				foto:"https://i.ibb.co/zJqVwDB/opera-Auriga-di-Delfi-2010-2102-102-A-1.jpg",
				descrizione:"“Gode egli i premî; ed il grembo\r\nparrasio, fra il popolo accolto,\r\na lui la vittoria\r\ngridò”.\r\n\r\nCon queste parole il poeta greco Pindaro descrive il vincitore di una gara tenutasi durante i giochi Panellenici, competizioni sportive a carattere sacro che coinvolgevano tutte le città della Grecia.\r\n\r\nAl Museo Omero è conservata la copia dal calco al vero, in gesso, dell’Auriga di Delfi. L’originale, in bronzo, faceva parte di un gruppo scultoreo raffigurante il guidatore di un carro (auriga) trainato quattro cavalli, vincitore di una gara. La scultura, dedicata al dio Apollo, fu probabilmente commissionata da Polizalos, tiranno di Gela. In origine collocata nel tempio di Apollo a Delfi è oggi conservata nel Museo Archeologico della città.\r\n\r\nLa statua dell’Auriga è la sola parte rimasta dell’intero complesso scultoreo. L’atleta è in piedi e indossa una lunga e pesante tunica a pieghe verticali, stretta in vita, che nasconde le forme del corpo. Il braccio sinistro è andato perduto; quello destro, nudo e proteso in avanti, rappresenta il solo accenno di movimento; la mano è racchiusa nell’atto di stringere le redini.\r\n\r\nUna modellazione precisa sottolinea con minuzia i tendini e le vene nelle parti scoperte del corpo: il braccio e i piedi. Il bel viso, dalla forma regolare, è incorniciato dalla capigliatura formata da riccioli tenuti sulla fronte da una fascia, decorata con il motivo geometrico modulare del meandro.\r\n\r\nI particolari venivano rifiniti col bulino solo dopo la fusione della statua, così da ottenere una maggior precisione nei dettagli più minuti. I giochi atletici erano considerati sacri dal popolo greco: le offerte votive erano una sorta di dono agli dei, intesi come benefattori e protettori degli uomini.",
				visualizzazioni:8
				),

						new(
				sala:"Greco e Romano",
				nome:"Afrodite di Milo",
				autore:"Morognola Calchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 202f, 84, 70f },
				tecnica:new(){ "calco al vero"},
				materiali:new(){"gesso","resina"},
				foto:"https://i.ibb.co/6Npkz5q/opera-afrodite-di-milo-2.jpg",
				descrizione:"“te, dea, te fuggono i venti, te le nubi del cielo\r\ne il tuo arrivo, sotto di te la terra operosa soavi\r\nfiori distende, a te sorridono le distese del mare\r\ne, rasserenato, il cielo risplende di luce diffusa.”\r\nLucrezio, De Rerum natura\r\n\r\nLa celebre Venere di Milo, scolpita intorno al 130 avanti Cristo, fu ritrovata nell’isola di Milo nel 1820. Questa raffigurazione della dea, diventata un’icona della bellezza femminile, sembra essere una copia di una scultura scomparsa di Lisippo.\r\nAl Museo Omero ne sono esposte ben due copie da calco al vero: la prima in gesso acquistata dal Museo, la seconda, in resina, in comodato d’uso dal Louvre, che ne conserva l’originale.\r\n\r\nLa dea, alta poco più di 2 metri, è rappresentata in posizione eretta, col torso nudo e coperta, dal bacino ai piedi, da una pesante veste, ricca di pieghe. La postura è sinuosa e l’intera figura disegna una sorta di S. Il peso è sostenuto dalla gamba destra, diritta, mentre la sinistra è spostata leggermente in avanti, flessa e ruotata, in atteggiamento pudico. La spalla destra è avanzata e abbassata rispetto all’altra: si disegna così una linea obliqua delle spalle, ripresa dalla veste a livello del bacino.\r\nLa testa, leggermente girata a sinistra, è caratterizzata da un bel viso regolare ovale, dai tipici lineamenti greci, come il naso lungo e diritto. L’impassibilità degna di una divinità è sottolineata dall’inespressività del volto, tipico dell’età classica. La capigliatura, fissata da un nastro, è finemente modellata con senso descrittivo e naturalistico, come la ciocca che, fuoriuscita dal nastro, pende sul collo.\r\n\r\nAl tatto l’opera è liscia e compatta e si riesce a cogliere l’infittirsi del drappeggio, all’altezza delle anche. Espediente che servì allo scultore per nascondere la giuntura tra i due blocchi di marmo.\r\n\r\nLa prima raffigurazione della Dea Afrodite “nuda” sembra esser stata scolpita da Prassitele: si tratta dell’Afrodite Cnidia, scolpita in marmo nel 360 avanti Cristo circa, collocata in origine nel tempio dedicato alla Dea a Cnido, antica città dell’Asia minore. La statua venne presa successivamente come modello per le molte raffigurazioni della Dea scolpite nel Periodo Ellenistico. Tra queste una delle più celebri è sicuramente l’Afrodite di Milo, raffigurazione della dea della sensualità e dell’amore, simbolo dell’antica bellezza femminile.",
				visualizzazioni:16
				),

						new(
				sala:"Greco e Romano",
				nome:"Nike di Samotracia",
				autore:"Morognola Calchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 245f, 280, 230 },
				tecnica:new(){ "calco al vero"},
				materiali:new(){"gesso alabastrino"},
				foto:"https://i.ibb.co/ypdDH8F/opera-Pythokritos-Nike-di-Samotracia-2010-2103.jpg",
				descrizione:"“Acefala, senza braccia, separata dalla sua mano che è recupero recente, consunta da tutte le raffiche delle Sporadi, la Vittoria di Samotracia è divenuta meno donna e più vento di mare ed aria”, Marguerite Youncenar da “Il tempo grande scultore”.\r\n\r\nLa Nike di Samotracia è una scultura che rappresenta la dea alata della vittoria nell’atto di spiccare il volo o di posarsi sulla prua di una nave. La collezione del Museo Omero possiede due versioni dell’opera: la prima è una copia da calco al vero delle stesse dimensioni dell’originale (alto 280 centimetri), la seconda una riduzione in scala 1:3, di grande aiuto nell’esplorazione tattile.\r\n\r\nL’opera originale è stata scolpita nel marmo di Rodi dallo scultore Pitocrito, della Scuola di Rodi, nel 190 avanti Cristo per commemorare le vittorie riportate dalla flotta di Rodi su Antioco terzo, Re della Siria. In origine era posta sulla prua di una grande nave di marmo, su una collinetta di fronte al Santuario dei Cabiri, nell’isola greca di Samotracia.\r\n\r\nIl corpo femminile è coperto da un leggero chitone, la tipica tunica greca, ed è caratterizzato da due grandi ali, mentre la testa e le braccia sono andate perdute; nonostante la sua incompletezza è un’opera di grande fascino. La figura, eretta, poggia il peso sulla gamba destra, mentre la sinistra è arretrata come per cercare stabilità o darsi lo slancio necessario per librarsi in aria. Il torace è spinto in avanti, mentre le ali, al contrario, si aprono dietro la schiena. A causa della perdita delle braccia possiamo solo supporre che azione stesse compiendo la dea: probabilmente la destra era sollevata e innalzava una corona o una tromba, nella quale soffiava.\r\n\r\nIl fascino di questa Nike è dato soprattutto dalla sua veste, il cui panneggio ci permette di percepire l’invisibile presenza del vento che investe la divinità, sostenendone il volo. Il chitone aderisce al tronco e alle gambe della Nike a causa del vento, lasciando intravedere i suoi seni, le curve del ventre, il leggero infossamento dell’ombelico. Alcuni lembi della veste si agitano invece dietro le spalle e le gambe della dea, contribuendo a suggerire la presenza di una corrente. Tutti questi elementi, percepibili chiaramente attraverso l’esplorazione tattile, contribuiscono a dare un senso di dinamismo e leggerezza all’opera, tratti tipici del tardo Ellenismo.",
				visualizzazioni:4
				),

						new(
				sala:"Greco e Romano",
				nome:"Zeus o Poseidone di Capo Artemisio ",
				autore:"Verdetti Ri-Calchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 209f, 250, 230 },
				tecnica:new(){ "calco al vero"},
				materiali:new(){"bronzo"},
				foto:"https://i.ibb.co/mvTYdZ2/opera-Poseidone-di-Capo-Artemisio-2010-2808-POSEID-2-903x1024.jpg",
				descrizione:"“Abitava in fondo al mare e comandava sui mostri marini e sulle tempeste, rappresentato spesso su un carro trainato da tritoni e cavalli con un tridente nella mano destra”, Cicerone dal “De Natura Deorum”.\r\n\r\nIl Poseidone o Zeus di Capo Artemisio, databile attorno il 460 avanti Cristo, rappresenta una delle poche opere scultoree in bronzo di età severa arrivate sino ai nostri giorni in buono stato di conservazione. Al Museo Omero ne è presente una copia in resina con fibra di vetro.\r\n\r\nLa scultura originale, attribuibile forse allo scultore greco Kalamides, è stata ritrovata in due frammenti tra il 1926 e il 1928 presso Capo Artemisio (la punta nord dell’isola di Eubea nel mar Egeo). Potrebbe rappresentare Zeus o più probabilmente Poseidone: nel luogo del ritrovamento si svolgevano riti legati al culto del dio del mare.\r\n\r\nLa divinità è colta nel momento di scagliare il tridente o il fulmine, elemento iconografico, purtroppo non conservato.\r\n\r\nLa figura è eretta, con gambe, petto e braccia rivolte verso chi guarda. Le gambe, leggermente flesse, sono divaricate per trovare il giusto equilibrio. Le braccia sono sollevate quasi a formare una croce: il sinistro è teso in avanti, come a prendere la mira, mentre il destro è flesso come se stesse per scagliare il tridente. Lo sguardo è rivolto in avanti, il volto teso e impassibile, concentrato nell’azione che sta per compiere. La barba è definita tramite riccioli allungati e sinuosi, che incorniciano con un ritmo decorativo il viso. Molto probabilmente in origine gli occhi erano costituiti da pasta vitrea o pietre dure policrome incastonate. L’opera non sembra pensata per esser vista solo da un lato: infatti anche i muscoli della schiena e delle spalle sono modellati in maniera accurata.\r\n\r\nLa statua testimonia l’alto livello di modellazione della forma umana a cui erano giunti gli scultori greci in età severa, seppur non rivela la tensione del movimento repentino e violento che si appresta a compiere, risultando immobile. Tra le caratteristiche della scultura greca di età severa: il massiccio uso del bronzo e l’antichissima tecnica della fusione a cera persa.",
				visualizzazioni:5
				),

						new(
				sala:"Greco e Romano",
				nome:"Discoforo",
				autore:"Morognola Calchi",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{ 175, 89f, 53f },
				tecnica:new(){ "calco al vero"},
				materiali:new(){"gesso alabastrino"},
				foto:"https://i.ibb.co/j6myZCL/opera-Naukydes-Discoforo-2010-2201-121-1-877x1536.jpg",
				descrizione:"“La bellezza nasce dall’esatta proporzione non degli elementi, ma delle parti… di tutte le parti tra loro come è scritto nel Canone di Policleto”, Galeno.\r\n\r\nIl “Discoforo”, ovvero il portatore di disco, raffigura un atleta greco in un momento di riposo. La scultura a tutto tondo è stata realizzata in età romana da un originale, scomparso, fuso in bronzo dallo scultore greco Naukides. Di cultura dorica, probabilmente allievo della scuola di Policleto, fu attivo tra il 420 e il 390 avanti Cristo circa.\r\n\r\nAl Museo Omero è presente una copia in gesso del Discoforo. Alto 175 centimetri, è rappresentato in piedi, frontale, con la gamba destra che compie un passo in avanti, mentre la sinistra, leggermente flessa, regge il peso della figura. Il braccio destro è piegato a novanta gradi e la sua mano sembra stringere un oggetto oggi perduto (forse un giavellotto); il braccio sinistro è disteso lungo il fianco e la mano regge il disco.\r\n\r\nLa testa, cinta da una capigliatura mossa, è leggermente abbassata e girata sulla sua destra. Gli occhi, aperti, sembrano guardare in basso. Il volto, ovale e carnoso, appare totalmente inespressivo, quasi senza vita, come volevano i canoni del periodo secondo la ricerca di una bellezza spirituale e idealizzante.\r\n\r\nLa figura atletica presenta una posa che richiama lo schema detto “chiasmo”, una formula compositiva, che consisteva nella disposizione degli arti secondo una particolare cadenza per risolvere il problema dell’equilibrio della figura eretta. Ad un arto inferiore flesso doveva corrispondere un arto superiore del lato opposto a riposo, secondo un gioco di contrapposizioni. Il termine, di origine greca, tradotto significa “disposizione a forma di chi”, riferito alla forma della ventiduesima lettera dell’alfabeto greco.\r\n\r\nQuesta figura di atleta dimostra come si potesse applicare la regola, o Canone, ideata da Policleto, al fine di rappresentare l’essere umano nella sua totale anatomia innalzandolo ad un concetto di perfezione difficilmente riscontrabile in natura.",
				visualizzazioni:7
				),

		};
		foreach (var opera in opere)
			await db.Post("opere", opera);
	}

	public async Task populateMostre()
	{
		var mostre = new List<Mostra>()
		{
			new(
				dataAggiunta: DateTime.Now,
				dataInizio: DateTime.Today.AddDays(10),
				dataFine: DateTime.Today.AddDays(20),
				titolo: "Le Patamacchine",
				foto:"https://i.ibb.co/R6CfrKH/Mostra-2022-23-Patamacchine-1024x411.jpg",
			descrizione:"Il Museo Tattile Statale Omero ospita la mostra \"Le Patamacchine\", ovvero un salvavita indispensabile alla sopravvivenza dell'immaginario.\r\nSi tratta di un allestimento interattivo, ideato e realizzato dall'Associazione La luna al guinzaglio di Potenza, Salone dei Rifiutati.\r\nLa collezione di opere è ispirata alle macchine inutili dello scultore svizzero Jean Tinguely e ai principi della Patafisica di Alfred Jarry, ovvero alla \"Scienza delle soluzioni immaginarie\".\r\nInaugurazione\r\n\r\nLa mostra è stata inaugurata martedì 6 dicembre alle ore 17:00. Insieme al Presidente del Museo Omero, Aldo Grassini, e allo staff del museo erano presenti Sara Stolfi della cooperativa il Salone dei Rifiutati e Mariangela Tolve dell'Associazione La luna al guinzaglio, che hanno guidato gli ospiti alla scoperta della mostra.\r\nMacchine immaginarie\r\n\r\nLe Patamacchine, come \"Il catalogatore di sogni\", \"Il pacificaphone\", \"Il potenziatore di autostima\", sono oggetti meccanici interamente costruiti con materiale di scarto o usato, in particolare con i Raee (Rifiuti di apparecchiature elettriche ed elettroniche).\r\n\r\nSono macchine alimentate dall'immaginario, sospese tra realtà e non sense, assurde, ironiche, in grado di divertire, incuriosire e creare spiazzamenti percettivi. Macchine con cui interagire, attraverso un apposito libretto di istruzioni. Ogni oggetto contiene potenzialità creative meritevoli di essere accolte e sviluppate, perché ogni cosa può essere riscoperta nelle sue mille possibilità.\r\n\r\nLa mostra intende far riflettere il visitatore sull'importanza delle relazioni umane e del rispetto ambientale, opponendosi alla logica dell'usa e getta con la creatività e la fantasia. Il futuro del nostro ambiente dipende dal modo in cui lo viviamo e dalla dimensione ecologica che riusciremo ad esprimere.\r\n\r\nLo staff del Museo Omero ti guiderà in mostra per mettere alla prova la tua immaginazione.\r\nRegala \"Le Patamacchine\"\r\n\r\nAbbiamo predisposto un buono da regalare a chi desideri per far vivere l'esperienza unica della mostra con una visita guidata.\r\nVieni a ritirarlo al Museo Omero: scegli tu il numero di persone a cui donarlo e chi lo riceve sceglierà la data di ingresso.\r\nInfo e prenotazioni\r\n\r\nPeriodo: dal 7 dicembre 2022 al 12 marzo 2023.\r\nOrario: dal martedì al sabato 16:00 - 19:00; domenica e festivi (26 dicembre, 6 gennaio) 10:00 - 13:00 e 16:00 - 19:00; 1° Gennaio: 16:00 -19:00.\r\nChiuso: lunedì; 24, 25 e 31 Dicembre.\r\nPrenotazione obbligatoria: Telefono e Whatsapp 335 56 96 985, Email prenotazioni@museoomero.it\r\nEtà consigliata: 0-99 anni.\r\nAl Costo di 5 euro è possibile usufruire della visita guidata interattiva alla mostra e dell'ingresso alla collezione Design. Gratuito: 0-4 anni, persone con disabilità e chi li accompagna.",
			opere: new(){ "-NOvamLQOLqRB9sRruLp", "-NOvamVofQQR_Mc7WjBl", "-NOvamhIclHgTEPPIuF8"}),

			new(
				dataAggiunta: DateTime.Now,
				dataInizio: DateTime.Today.AddDays(20),
				dataFine: DateTime.Today.AddDays(30),
				titolo: "Pasolini pittore",
				foto:"https://i.ibb.co/Y8cyCLR/eventi2022-Pasolini-pittore-1024x411.jpg",
			descrizione:"A Roma, presso la Galleria d'Arte Moderna è in corso \"Pasolini pittore\" un progetto espositivo esclusivo completamente inedito nel suo genere, ideato per i cento anni dalla nascita di Pier Paolo Pasolini (1922-1975). La mostra intende riportare l'attenzione su un aspetto artistico rilevante, spesso trascurato dalla critica, nel contesto creativo complessivo dello scrittore e regista.\r\n\r\nIn mostra oltre 150 opere, selezionate dal corpus della collezione del Gabinetto Scientifico Letterario G.P. Vieusseux di Firenze, depositario della raccolta maggiore di opere di Pasolini, ma anche dalla Fondazione Cineteca di Bologna, dal Centro Studi Pier Paolo Pasolini di Casarsa, per la prima volta in mostra fuori dalla locale Casa Colussi, dall'Archivio Giuseppe Zigaina, oltre che da collezionisti privati.\r\nAccessibilità\r\n\r\nUn'attenzione particolare è stata dedicata all'accessibilità: per le persone con disabilità visiva è stato progettato, in collaborazione con il Museo Tattile Statale Omero di Ancona, un percorso dedicato, dotato di 6 disegni a rilievo e relative audiodescrizioni. Saranno inoltre disponibili visite tattili gratuite, guidate da operatori specializzati.\r\n\r\nA corollario della mostra sarà organizzata una serie di incontri culturali, readings e proiezioni di compendio alle tematiche affrontate nella mostra dal titolo \"Pasoliniana. Intorno a Pasolini pittore\",\r\na cura di Silvana Cirillo e Claudio Crescentini.\r\n\r\nIl progetto, curato da Silvana Cirillo, Claudio Crescentini e Federica Pirani per la Galleria d'Arte\r\nModerna di Roma è promosso da Roma Culture, Sovrintendenza Capitolina ai Beni Culturali, \"Sapienza\" Università di Roma, Facoltà di Lettere e Filosofia, Dipartimento di Lettere e Culture moderne, Gabinetto Scientifico Letterario G.P. Vieusseux di Firenze, Centro Studi Pier Paolo Pasolini di Casarsa della Delizia (PN) e Fondazione Cineteca di Bologna, in collaborazione con l'Archivio Giuseppe Zigaina e l'organizzazione di Zètema Progetto Cultura. Radio Partner Dimensione Suono Soft.\r\nIl catalogo è edito da Silvana Editoriale.",
			opere: new(){ "-NOvamkQQgYkVrlsBkrz", "-NOvamnDCnhFY6pSJNtt", "-NOvamqSug2oAyHL3Fj7","-NOvamtdRm3tiTAqWP5K","-NOvamvFv5ruR6YDN3b9","-NOvamws1nDH4Y8IUddm","-NOvamyRRZtvsIMsWJUj"})
		};

		foreach (var mostra in mostre)
			await db.Post("mostre", mostra);
	}
}
