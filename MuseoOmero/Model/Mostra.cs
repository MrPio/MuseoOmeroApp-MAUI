namespace MuseoOmero.Model;
public class Mostra
{
	[JsonProperty("data_aggiunta")] public DateTime DataAggiunta { get; set; }
	[JsonProperty("data_inizio")] public DateTime DataInizio { get; set; }
	[JsonProperty("data_fine")] public DateTime DataFine { get; set; }
	[JsonProperty("titolo")] public string Titolo { get; set; }
	[JsonProperty("foto")] public string Foto { get; set; }
	[JsonProperty("descrizione")] public string Descrizione { get; set; }

	public Mostra(DateTime dataAggiunta, DateTime dataInizio, DateTime dataFine, string titolo, string foto, string descrizione)
	{
		DataAggiunta = dataAggiunta;
		DataInizio = dataInizio;
		DataFine = dataFine;
		Titolo = titolo;
		Foto = foto;
		Descrizione = descrizione;
	}
}
