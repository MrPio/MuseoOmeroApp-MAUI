namespace MuseoOmero.ViewModel.Templates;
public partial class RoundedButtonViewModel : ObservableObject
{
    [ObservableProperty]
    string text;

    [ObservableProperty]
    Func<Task> onClick;

	public RoundedButtonViewModel(string text,  Func<Task> onClick)
    {
        Text = text;
        OnClick = onClick;
    }

    public double FontSize { get; set; } = 18;
    public double HeightRequest { get; set; } = 52;
    public double CornerRadius { get; set; } =  26;

}
