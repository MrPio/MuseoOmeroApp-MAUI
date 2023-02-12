using MuseoOmero.Resources.Material;

namespace MuseoOmero.Model.Enums;
public class TipoBiglietto
{
    public static TipoBiglietto MuseoAperto { get { return new TipoBiglietto("Museo aperto", IconFont.TicketConfirmation); } }
    public static TipoBiglietto Mostra { get { return new TipoBiglietto("Mostra", IconFont.Paw); } }
    public static TipoBiglietto Laboratorio { get { return new TipoBiglietto("Laboratorio", IconFont.Puzzle); } }

    public TipoBiglietto(string value, string icon) { Value = value; Icon = icon; }
    public string Value { get; private set; }
    public string Icon { get; private set; }

    public override string ToString()
    {
        return Value;
    }


}
