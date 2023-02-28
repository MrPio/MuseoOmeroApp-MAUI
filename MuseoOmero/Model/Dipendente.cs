namespace MuseoOmero.Model;
public class Dipendente
{
	[JsonIgnore] public string Uid { get; set; }
	[JsonProperty("cellulare")] public string Cellulare { get; set; } = null;
	[JsonProperty("email")] public string Email { get; set; } = null;
	[JsonProperty("nome")] public string Nome { get; set; } = null;
	[JsonProperty("ruolo")] public string Ruolo { get; set; } = null;

	[JsonProperty("cognome")] public string Cognome { get; set; } = null;
	[JsonProperty("last_online")] public DateTime LastOnline { get; set; }

	[JsonConstructor]
	public Dipendente( string nome, string cognome, string cellulare, string email, string ruolo, DateTime lastOnline)
	{
		Nome = nome;
		Cognome = cognome;
		Cellulare = cellulare;
		Email = email;
		Ruolo = ruolo;
		LastOnline = lastOnline;
	}

	public Dipendente(string uid, string nome, string cognome, string cellulare, string email, string ruolo, DateTime lastOnline)
	{
		Uid = uid;
		Nome = nome;
		Cognome = cognome;
		Cellulare = cellulare;
		Email = email;
		Ruolo = ruolo;
		LastOnline = lastOnline;
	}
}
