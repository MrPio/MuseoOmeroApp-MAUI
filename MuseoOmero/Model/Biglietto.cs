namespace MuseoOmero.Model;
public class Biglietto
{
	[JsonProperty("uid")] public string Uid { get; set; }

	[JsonProperty("data_acquisto")] public DateTime DataAcquisto { get; set; }

	[JsonProperty("data_validita")] public DateTime DataValidita { get; set; } //il giorno in cui il biglietto può essere utilizzato

	[JsonProperty("data_convalida")] public DateTime? DataConvalida { get; set; }

	[JsonProperty("tipologia")] public TipoBiglietto Tipologia { get; set; }=TipoBiglietto.MuseoAperto;

	[JsonProperty("data_guida")] public TimeSpan? OrarioGuida { get; set; } = null;

	public Biglietto(DateTime dataAcquisto, DateTime dataValidita, TipoBiglietto tipologia, DateTime? dataConvalida = null, TimeSpan? dataGuida = null)
	{
		Uid = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString();
		DataAcquisto = dataAcquisto;
		DataValidita = dataValidita;
		Tipologia = tipologia;
		DataConvalida = dataConvalida;
		OrarioGuida = dataGuida;
	}

	[JsonConstructor]
	public Biglietto(string uid, DateTime dataAcquisto, DateTime dataValidita, DateTime? dataConvalida, TipoBiglietto tipologia, TimeSpan? dataGuida)
	{
		Uid = uid;
		DataAcquisto = dataAcquisto;
		DataValidita = dataValidita.AddHours(12).Date;//PER PROBLEMI DI FUSO ORARIO +-1GMT
		DataConvalida = dataConvalida;
		Tipologia = tipologia;
		OrarioGuida = dataGuida;
	}

	public bool IsValido => DataValidita.Date >= DateTime.Today;
	public bool IsConvalidabile => DataValidita.Date == DateTime.Today;
}
