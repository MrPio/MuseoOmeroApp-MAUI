namespace MuseoOmero.Model;

public class Utente
{
	[JsonIgnore] public string Uid { get; set; }
	[JsonProperty("username")] public string Username { get; set; }
	[JsonProperty("cellulare")] public string Cellulare { get; set; } = null;
	[JsonProperty("nome")] public string Nome { get; set; } = null;
	[JsonProperty("cognome")] public string Cognome { get; set; } = null;
	[JsonProperty("biglietti")] public List<Biglietto> Biglietti { get; set; }
	[JsonProperty("questionari")] public List<Questionario> Questionari { get; set; }
	[JsonProperty("last_online")] public DateTime LastOnline { get; set; }
	[JsonProperty("chat")] public Chat? Chat { get; set; }

	public Utente(string username, string nome, string cognome, string cellulare, List<Biglietto> biglietti, List<Questionario> questionari, Chat chat, DateTime lastOnline)
	{
		Username = username;
		Cellulare = cellulare;
		Nome = nome;
		Cognome = cognome;
		Biglietti = biglietti is null ? new() : biglietti;
		Questionari = questionari is null ? new() : questionari;
		Chat = chat;
		LastOnline = lastOnline;
	}
}
