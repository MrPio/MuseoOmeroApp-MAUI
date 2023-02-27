namespace MuseoOmero.Model;
public partial class Messaggio : ObservableObject
{
	[JsonProperty("data")]
	[ObservableProperty]
	public DateTime data;
	[ObservableProperty]

	[JsonProperty("testo")] public string testo;
	[ObservableProperty]

	[JsonProperty("letto")] public bool letto = false;

	[JsonConstructor]
	public Messaggio(DateTime data, string testo, bool letto)
	{
		Data = data;
		Testo = testo;
		Letto = letto;
	}

	public Messaggio(DateTime data, string testo)
	{
		Data = data;
		Testo = testo;
	}
}