namespace MuseoOmero.Model{
public class Opera
{
	public string Id { get; set; }
	[JsonProperty("sala")] public string Sala { get; set; }
	[JsonProperty("nome")] public string Nome{ get; set; }
	[JsonProperty("autore")] public string Autore { get; set; }
	[JsonProperty("data_aggiunta")] public DateTime DataAggiunta { get; set; }
	[JsonProperty("dimensioni")] public float[] Dimensioni { get; set; }
	[JsonProperty("tecnica")] public List<string> Tecnica { get; set; }
	[JsonProperty("materiali")] public List<string> Materiali { get; set; }
	[JsonProperty("foto")] public string Foto{ get; set; }
	[JsonProperty("descrizione")] public string Descrizione { get; set; }

	[JsonProperty("visualizzazioni")] public int Visualizzazioni { get; set; }

	public Opera(string sala, string nome, string autore, DateTime dataAggiunta, float[] dimensioni, List<string> tecnica, List<string> materiali, string foto, string descrizione, int visualizzazioni)
	{
		Sala = sala;
		Nome = nome;
		Autore = autore;
		DataAggiunta = dataAggiunta;
		Dimensioni = dimensioni;
		Tecnica = tecnica;
		Materiali = materiali;
		Foto = foto;
		Descrizione = descrizione;
		Visualizzazioni = visualizzazioni;
	}
}}
