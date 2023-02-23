using MuseoOmero.Managers;
using MuseoOmero.ViewModel.Templates;

namespace MuseoOmero.View.Templates;

public partial class MyEntryView : ContentView
{
    ResourceDictionary MyColors = Application.Current.Resources.MergedDictionaries.First();
    MyEntryViewModel viewModel { get => BindingContext as MyEntryViewModel; }
    private DeviceManager deviceManager { get => DeviceManager.Instance; }
    public MyEntryView()
    {
        InitializeComponent();
    }

	private void Entry_Focused(object sender, FocusEventArgs e)
    {
        ((Entry)sender).TextColor = Color.FromHex("#1e1e1e");
        Icon.TextColor = deviceManager.Colors[1];
        Border.Stroke = deviceManager.Colors[1];
        Border.StrokeThickness = viewModel.BorderTicknessFocused;
        Border.BackgroundColor = Colors.White;
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        ((Entry)sender).TextColor = Color.FromHex("#686868");
        Icon.TextColor = viewModel.EntryBorderColor;
        Border.Stroke = viewModel.EntryBorderColor;
        Border.StrokeThickness = viewModel.BorderTicknessUnfocused;
        Border.BackgroundColor = Colors.White;
    }

    private void DatePicker_Focused(object sender, FocusEventArgs e)
    {
        ((DatePicker)sender).TextColor = Color.FromHex("#1e1e1e");
        Icon.TextColor = deviceManager.Colors[1];
        Border.Stroke = deviceManager.Colors[1];
        Border.StrokeThickness = viewModel.BorderTicknessFocused;
        Border.BackgroundColor = Colors.White;
    }

    private void DatePicker_Unfocused(object sender, FocusEventArgs e)
    {
        ((DatePicker)sender).TextColor = Color.FromHex("#686868");
        Icon.TextColor = viewModel.EntryBorderColor;
        Border.Stroke = viewModel.EntryBorderColor;
        Border.StrokeThickness = viewModel.BorderTicknessUnfocused;
        Border.BackgroundColor = Colors.White;
    }

}