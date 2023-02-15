namespace MuseoOmero.Model;
public class Questionario
{
	[JsonProperty("tipologia_visita")] public string TipologiaVisita { get; set; }
	[JsonProperty("accompagnatori_visita")] public string AccompagnatoriVisita { get; set; }
	[JsonProperty("motivazione_visita")] public string MotivazioneVisita { get; set; }
	[JsonProperty("titolo_studi")] public string TitoloStudi { get; set; }
	[JsonProperty("numero_visite")] public int NumeroVisite { get; set; }
	[JsonProperty("ritorno")] public string Ritorno { get; set; }
	[JsonProperty("valutazione_visita")] public int ValutazioneVisita { get; set; }
	[JsonProperty("valutazione_esperienza")] public int ValutazioneEsperienza { get; set; }
	[JsonProperty("valutazione_struttura")] public int ValutazioneStruttura { get; set; }
	[JsonProperty("data_compilazione")] public DateTime DataCompilazione { get; set; }

	public Questionario(string tipologiaVisita, string accompagnatoriVisita, string motivazioneVisita, string titoloStudi, int numeroVisite, string ritorno, int valutazioneVisita, int valutazioneEsperienza, int valutazioneStruttura, DateTime dataCompilazione)
	{
		TipologiaVisita = tipologiaVisita;
		AccompagnatoriVisita = accompagnatoriVisita;
		MotivazioneVisita = motivazioneVisita;
		TitoloStudi = titoloStudi;
		NumeroVisite = numeroVisite;
		Ritorno = ritorno;
		ValutazioneVisita = valutazioneVisita;
		ValutazioneEsperienza = valutazioneEsperienza;
		ValutazioneStruttura = valutazioneStruttura;
		DataCompilazione = dataCompilazione;
	}
}
