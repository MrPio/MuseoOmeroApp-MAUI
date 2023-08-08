namespace MuseoOmero.Model{
public class Chat
{
	//[JsonIgnore] public string Utente { get; set; }
	[JsonProperty("messaggi_museo")] public List<Messaggio> MessaggiMuseo { get; set; }
	[JsonProperty("messaggi_utente")] public List<Messaggio> MessaggiUtente { get; set; }
	[JsonProperty("data_inizio")] public DateTime DataInizio { get; set; }

	[JsonConstructor]
	public Chat( List<Messaggio> messaggiMuseo, List<Messaggio> messaggiUtente, DateTime dataInizio)
	{
		MessaggiMuseo = messaggiMuseo??=new();
		MessaggiUtente = messaggiUtente ??= new();
		DataInizio = dataInizio;
	}
}
}