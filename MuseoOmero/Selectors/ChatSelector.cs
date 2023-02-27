namespace MuseoOmero.Selectors;
	public class ChatSelector : DataTemplateSelector
	{
		public DataTemplate SentTemplate { get; set; }
		public DataTemplate ReceivedTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return ((MessaggioConMittente)item).DiUtente ? ReceivedTemplate : SentTemplate;
		}
	}
