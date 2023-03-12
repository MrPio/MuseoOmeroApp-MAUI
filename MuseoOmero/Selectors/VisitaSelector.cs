namespace MuseoOmero.Selectors;

public class VisitaSelector : DataTemplateSelector
{
	public DataTemplate VisitaSenzaQuestionario { get; set; }
	public DataTemplate VisitaConQuestionario { get; set; }

	protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
	{
		return ((Visita)item).Questionario is { } ? VisitaConQuestionario : VisitaSenzaQuestionario;
	}
}
