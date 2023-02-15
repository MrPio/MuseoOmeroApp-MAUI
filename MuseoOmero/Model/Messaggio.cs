namespace MuseoOmero.Model;
public class Messaggio
{
	[JsonProperty("data")] public DateTime Data { get; set; }
	[JsonProperty("testo")] public string Testo { get; set; }
	[JsonProperty("letto")] public bool Letto { get; set; }

	public Messaggio(DateTime data, string testo)
	{
		Data = data;
		Testo = testo;
	}
}