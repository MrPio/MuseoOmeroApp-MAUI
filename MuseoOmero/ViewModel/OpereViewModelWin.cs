namespace MuseoOmero.ViewModelWin;

public partial class OpereViewModelWin : ObservableObject
{
	private HomeViewModelWin _homeViewModel;

	[ObservableProperty]
	string filtroOpere = "Titolo", filtroMostre = "Titolo";

	[ObservableProperty]
	ObservableCollection<Opera> opereOrdinate = new();

	[ObservableProperty]
	ObservableCollection<Mostra> mostreOrdinate = new();

	[ObservableProperty]
	bool opereSortAcending = true, mostreSortAcending = true;

	[ObservableProperty]
	bool opereOn = true,mostreOn=false;

	[ObservableProperty]
	Opera selectedOpera =new (
				sala:"Ingresso",
				nome:"Ingranaggio",
				autore:"Umberto Mastroianni",
				dataAggiunta:DateTime.Now,
				dimensioni:new[]{29f,20f,9.5f },
				tecnica:new (){ "fusione","doratura","lucidatura"},
				materiali: new() { "bronzo" },
				foto: "https://i.ibb.co/fGrdTHY/opera-Mastroianni-Ingranaggio-2010-1502-A-1024x1536-1.jpg",
				descrizione: "“Oggi la riapparizione di una\r\nforma è quasi leggenda.\r\n\r\nOggi la rinascita di una\r\nforma può\r\nriportare al vertice il\r\ndivenire di un pensiero\r\npuro originale\r\narrivato sulla terra stanca”.\r\nUmberto Mastroianni\r\n\r\n“Ingranaggio”, opera inserita all’interno della sezione itinerante “Bello e Accessibile” del Museo Omero, è una piccola scultura in bronzo (alta 20 centimetri) realizzata da Umberto Mastroianni nel 1988.\r\n\r\nInnestata su una base in marmo bianco a forma di parallelepipedo, la scultura è un assemblaggio di forme astratte costruite su linee curve e spezzate. Dalla base si innalzano forme dallo slancio verticale, con curve tondeggianti e sporgenti sul profilo sinistro, e rientranti e spigolose sullo quello destro. Forme che accompagnano l’occhio e la mano verso due cerchi, scavati all’interno, uno a destra uno a sinistra. Da questi cerchi, che ricordano due “anelli”, si aprono sulla sommità due “corone dentate”. Sopra l’anello di sinistra, più grande (4 centimetri di diametro) si apre la corona con cinque denti di varie dimensioni, simili ai merli delle architetture militari medievali, sopra l’anello di destra (3 centimetri di diametro) si apre la corona più piccola con tre denti.\r\nLa superficie del bronzo è lucida e levigata, liscia e piacevole al tatto. Sul retro, lungo la forma verticale che sale dal basamento della scultura, è incisa la firma dell’artista.\r\n\r\nUn insieme di forme che potrebbe suggerire la visione di un essere zoomorfo, un animale astratto, con becco aperto, cresta e occhio. Sicuramente l’effetto generale è di grande dinamismo e, come suggerisce il titolo, ci riporta alla mente un ingranaggio, un meccanismo in movimento. L’opera è parte di una lunga serie di studi sulla macchina e il suo dinamismo, che l’artista pone al centro della propria ricerca artistica ed estetica.\r\n“Ingranaggio” si inserisce nella penultima fase creativa di Mastroianni: dopo gli anni 60/70, periodo delle esplosioni violente, dalla plasticità dirompente, nelle sue creazioni appaiono ingranaggi, meccanismi integrati a forme geometriche inedite, non prive di senso ironico, che lo porta di frequente a suggerire l’esistenza di un fantastico mondo di animali meccanicizzati.",
				visualizzazioni: 4
				);

[ObservableProperty]
	Mostra selectedMostra;

	[RelayCommand]
	void HeaderLabelTap(string titolo)
	{
		if (FiltroOpere == titolo)
			OpereSortAcending = !OpereSortAcending;
		else
			OpereSortAcending = true;
		FiltroOpere = titolo;
		OrdinaOpere();
	}
	[RelayCommand]
	void InvertSortTap()
	{
		OpereSortAcending = !OpereSortAcending;
		OrdinaOpere();
	}

	[RelayCommand]
	void HeaderLabelMostreTap(string titolo)
	{
		if (FiltroMostre == titolo)
			MostreSortAcending = !MostreSortAcending;
		else
			MostreSortAcending = true;
		FiltroMostre = titolo;
		OrdinaMostre();
	}
	[RelayCommand]
	void InvertSortMostreTap()
	{
		MostreSortAcending = !MostreSortAcending;
		OrdinaMostre();
	}

	public void OrdinaOpere()
	{
		Func<Opera, dynamic> opereFunc = FiltroOpere switch
		{
			"DataReg" => o => o.DataAggiunta,
			"Sala" => o => o.Sala,
			"Autore" => o => o.Autore,
			"Visuals" => o => o.Visualizzazioni,
			_ => o => o.Nome
		};
		var opereOrdinate = _homeViewModel.Opere.OrderBy(opereFunc).ToList();
		if (!OpereSortAcending)
			opereOrdinate.Reverse();
		OpereOrdinate = new ObservableCollection<Opera>(opereOrdinate.ToList());
	}
	public void OrdinaMostre()
	{
		Func<Mostra, dynamic> mostreFunc = FiltroMostre switch
		{
			"DataReg" => m => m.DataAggiunta,
			"Sala" => m => m.DataInizio,
			"Autore" => m => m.DataFine,
			_ => m => m.Titolo
		};
		var mostreOrdinate = _homeViewModel.Mostre.OrderBy(mostreFunc).ToList();
		if (!MostreSortAcending)
			mostreOrdinate.Reverse();
		MostreOrdinate = new ObservableCollection<Mostra>(mostreOrdinate.ToList());
	}
	public OpereViewModelWin(HomeViewModelWin homeViewModelWin)
	{
		_homeViewModel = homeViewModelWin;
	}
}
