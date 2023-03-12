namespace MuseoOmero.Model.Enums;
public enum TipoBiglietto
{
	MuseoAperto = 0,
	Mostra = 1,
	Laboratorio = 2
}

public static class Sala
{
	public static readonly string[] Values ={"Ingresso", "Greco e Romano", "Mimica del volto umano", "Ancona", "Medievale e '400", "Rinascimentale", "Dipinti", "'900 e Contemporaneo", "Movimento scolpito", "Deposito"
	};
}

public static class Materiale
{
	public static readonly string[] Values =
		"bronzo\r\ngesso\r\ngesso alabastrino\r\ngesso patinato\r\nlegno\r\nmetallo\r\nottone\r\nresina\r\nspecchio\r\nterracotta\r\nvetro".Split("\r\n");

}

public static class Tecnica
{
	public static readonly string[] Values =
		"assemblaggio\r\ncalco\r\ncalco al vero\r\ndoratura\r\nfusione\r\nincisione\r\nintaglio\r\npatinatura".Split("\r\n");

}


public static class IconeBiglietto
{
	public static readonly string[] Values ={
		IconFont.Ticket,
		IconFont.Paw,
		IconFont.Puzzle
	};
}

public static class TipologiaVisita
{
	public static readonly string[] Values ={
		"Visita guidata",
		"Visita libera",
		"Partecipazione ad una mostra/laboratorio",
	};
}

public static class AccompagnatoriVisita
{
	public static readonly string[] Values ={
		"solo", "partner/coniuge", "famiglia", "amici/parenti/conoscenti", "scolaresca", "gruppo organizzato"
	};
}

public static class MotivazioneVisita
{
	public static readonly string[] Values ={
		"vedere oggetti belli", "incontrare persone con interessi simili ai miei", "vedere oggetti importanti", "imparare cose nuove", "trascorrere tempo libero con amici/parenti", "approfondire le mie conoscenze", "passare un momento personale piacevole"
	};
}

public static class TitoloStudi
{
	public static readonly string[] Values ={
		"Elem.", "Lic. Media", "Diploma", "Laurea/PostLaurea", "Nessuno"
	};
}

public static class Ritorno
{
	public static readonly string[] Values ={
		"Assolutamente no", "Probabilmente No", "Probabilmente Si", "Assolutamente Si"
	};
}

public static class Valutazione
{
	public static readonly int[] Values = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
}



