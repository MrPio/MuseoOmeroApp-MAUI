namespace MuseoOmero.Model;
public class Laboratorio
{
	[JsonProperty("data_aggiunta")] public DateTime DataAggiunta { get; set; }
	[JsonProperty("durata_minuti")] public int DurataMinuti { get; set; }
	[JsonProperty("date")] public List<DateTime> Date { get; set; }
	[JsonProperty("titolo")] public string Titolo { get; set; }
	[JsonProperty("destinatari")] public List<string> Destinatari { get; set; }
	[JsonProperty("descrizione")] public string Descrizione { get; set; }

	[JsonProperty("prezzo")] public float Prezzo { get; set; }

	public Laboratorio(DateTime dataAggiunta, int durataMinuti, List<DateTime> date, string titolo, List<string> destinatari, string descrizione, float prezzo)
	{
		DataAggiunta = dataAggiunta;
		DurataMinuti = durataMinuti;
		Date = date;
		Titolo = titolo;
		Destinatari = destinatari;
		Descrizione = descrizione;
		Prezzo = prezzo;
	}
}
