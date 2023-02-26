namespace MuseoOmero.Model;
public class Biglietto
{
	[JsonProperty("uid")] public string Uid { get; set; }

	[JsonProperty("data_acquisto")] public DateTime DataAcquisto { get; set; }

	[JsonProperty("data_validita")] public DateTime DataValidita { get; set; } //il giorno in cui il biglietto può essere utilizzato

	[JsonProperty("data_convalida")] public DateTime? DataConvalida { get; set; }

	[JsonProperty("tipologia")] public TipoBiglietto Tipologia { get; set; } = null;

	[JsonProperty("data_guida")] public DateTime? DataGuida { get; set; } = null;

	public Biglietto(DateTime dataAcquisto, DateTime dataValidita, TipoBiglietto tipologia, DateTime? dataConvalida = null, DateTime? dataGuida = null)
	{
		Uid = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString();
		DataAcquisto = dataAcquisto;
		DataValidita = dataValidita;
		Tipologia = tipologia;
		DataConvalida = dataConvalida;
		DataGuida = dataGuida;
	}

	[JsonConstructor]
	public Biglietto(string  uid, DateTime dataAcquisto, DateTime dataValidita, DateTime? dataConvalida, TipoBiglietto tipologia, DateTime? dataGuida)
	{
		Uid = uid;
		DataAcquisto = dataAcquisto;
		DataValidita = dataValidita;
		DataConvalida = dataConvalida;
		Tipologia = tipologia;
		DataGuida = dataGuida;
	}
}
