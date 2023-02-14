using MuseoOmero.Model.Enums;
using Newtonsoft.Json;

namespace MuseoOmero.Models;

public class Biglietto
{
    [JsonProperty("data_acquisto")]
    public DateTime DataAcquisto { get; set; }

    [JsonProperty("data_validita")]
    public DateTime DataValidita { get; set; }

    [JsonProperty("tipologia")]
    public TipoBiglietto Tipologia { get; set; } = null;

		[JsonProperty("data_guida")]
    public DateTime? DataGuida { get; set; } = null;

    public Biglietto(DateTime dataAcquisto, DateTime dataValidita, TipoBiglietto tipologia, DateTime? dataGuida)
    {
        DataAcquisto = dataAcquisto;
        DataValidita = dataValidita;
        Tipologia = tipologia;
        DataGuida = dataGuida;
    }
}
