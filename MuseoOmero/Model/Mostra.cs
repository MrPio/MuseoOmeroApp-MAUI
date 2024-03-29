﻿namespace MuseoOmero.Model{
public class Mostra
{
	public string Id { get; set; }
	[JsonProperty("data_aggiunta")] public DateTime DataAggiunta { get; set; }
	[JsonProperty("data_inizio")] public DateTime DataInizio { get; set; }
	[JsonProperty("data_fine")] public DateTime DataFine { get; set; }
	[JsonProperty("titolo")] public string Titolo { get; set; }
	[JsonProperty("foto")] public string Foto { get; set; }
	[JsonProperty("descrizione")] public string Descrizione { get; set; }

	[JsonProperty("opere")] public List<string> Opere { get; set; }

	public Mostra( DateTime dataAggiunta, DateTime dataInizio, DateTime dataFine, string titolo, string foto, string descrizione, List<string> opere)
	{
		DataAggiunta = dataAggiunta;
		DataInizio = dataInizio;
		DataFine = dataFine;
		Titolo = titolo;
		Foto = foto;
		Descrizione = descrizione;
		Opere = opere;
	}
}}
