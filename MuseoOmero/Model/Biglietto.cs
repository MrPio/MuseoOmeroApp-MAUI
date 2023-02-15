namespace MuseoOmero.Models;
public class Biglietto
{
	[JsonProperty("data_acquisto")] public DateTime DataAcquisto { get; set; }

	[JsonProperty("data_validita")] public DateTime DataValidita { get; set; } //il giorno in cui il biglietto può essere utilizzato

	[JsonProperty("data_convalida")] public DateTime? DataConvalida { get; set; }

	[JsonProperty("tipologia")] public TipoBiglietto Tipologia { get; set; } = null;

	[JsonProperty("data_guida")] public DateTime? DataGuida { get; set; } = null;

	public Biglietto(DateTime dataAcquisto, DateTime dataValidita, TipoBiglietto tipologia, DateTime? dataConvalida = null, DateTime? dataGuida = null)
	{
		DataAcquisto = dataAcquisto;
		DataValidita = dataValidita;
		Tipologia = tipologia;
		DataConvalida = dataConvalida;
		DataGuida = dataGuida;
	}
}
