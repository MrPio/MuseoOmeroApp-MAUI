namespace MuseoOmero.Model;
public class Opera
{
	[JsonProperty("sala")] public string Sala { get; set; }
	[JsonProperty("nome")] public string Nome{ get; set; }
	[JsonProperty("autore")] public string Autore { get; set; }
	[JsonProperty("data_aggiunta")] public DateTime DataAggiunta { get; set; }
	[JsonProperty("dimensioni")] public float[] Dimensioni { get; set; }
	[JsonProperty("tecnica")] public string Tecnica { get; set; }
	[JsonProperty("materiali")] public string Materiali { get; set; }
	[JsonProperty("foto")] public string Foto{ get; set; }
	[JsonProperty("descrizione")]public string Descrizione { get; set; }

	public Opera(string sala, string nome, string autore, DateTime dataAggiunta, float[] dimensioni, string tecnica, string materiali, string foto, string descrizione)
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
	}
}
