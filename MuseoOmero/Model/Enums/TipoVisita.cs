namespace MuseoOmero.Model.Enums;
public class TipoVisita
{
	public static TipoVisita VisitaGuidata { get { return new TipoVisita("Visita guidata"); } }
	public static TipoVisita VisitaLibera { get { return new TipoVisita("Visita libera"); } }
	public static TipoVisita PartecipazioneAdUnaMostra_Laboratorio { get { return new TipoVisita("Partecipazione ad una mostra/laboratorio"); } }

	public TipoVisita(string value) { Value = value;  }
	public string Value { get; private set; }

	public override string ToString()
	{
		return Value;
	}
}
