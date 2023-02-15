namespace MuseoOmero.Model;
public class Libro
{
	[JsonProperty("data_aggiunta")] public DateTime DataAggiunta { get; set; }
	[JsonProperty("titolo")] public string Titolo { get; set; }
	[JsonProperty("foto")] public string Foto { get; set; }
	[JsonProperty("editore")] public string Editore { get; set; }
	[JsonProperty("pagine")] public int Pagine { get; set; }
	[JsonProperty("prezzo")] public float Prezzo { get; set; }
	[JsonProperty("disponibilita")] public int Disponibilita { get; set; }
	[JsonProperty("descrizione")] public string Descrizione { get; set; }

	public Libro(DateTime dataAggiunta, string titolo, string foto, string editore, int pagine, float prezzo, int disponibilita, string descrizione)
	{
		DataAggiunta = dataAggiunta;
		Titolo = titolo;
		Foto = foto;
		Editore = editore;
		Pagine = pagine;
		Prezzo = prezzo;
		Disponibilita = disponibilita;
		Descrizione = descrizione;
	}
}
