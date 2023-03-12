namespace MuseoOmero.Selectors;
public class ChatSelector : DataTemplateSelector
{
	public DataTemplate SentTemplate { get; set; }
	public DataTemplate ReceivedTemplate { get; set; }

	public bool Inverted { get; set; } = false;

	protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
	{
		return ((MessaggioConMittente)item).DiUtente ^ Inverted ? ReceivedTemplate : SentTemplate;
	}
}
