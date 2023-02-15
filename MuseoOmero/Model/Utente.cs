using Newtonsoft.Json;

namespace MuseoOmero.Model;

public class Utente
{
	public string Id { get; set; }
	[JsonProperty("username")] public string Username { get; set; }
	[JsonProperty("password")] public string Password { get; set; }
	[JsonProperty("email")] public string Email { get; set; }
	[JsonProperty("cellulare")] public string Cellulare { get; set; } = null;
	[JsonProperty("nome")] public string Nome { get; set; } = null;
	[JsonProperty("cognome")] public string Cognome { get; set; } = null;
	[JsonProperty("biglietti")] public List<Biglietto> Biglietti;
	[JsonProperty("questionari")] public List<Questionario> Questionari;
	[JsonProperty("chat")] public Chat? Chat;

	public Utente(string username, string password, string email, string cellulare, string nome, string cognome, List<Biglietto> biglietti, List<Questionario> questionari, Chat chat)
	{
		Username = username;
		Password = password;
		Email = email;
		Cellulare = cellulare;
		Nome = nome;
		Cognome = cognome;
		Biglietti = biglietti is null ? new() : biglietti;
		Questionari = questionari is null ? new() : questionari;
		Chat = chat;
	}
}
