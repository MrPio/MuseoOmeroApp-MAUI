using CommunityToolkit.Mvvm.ComponentModel;
using MuseoOmero.Managers;

namespace MuseoOmero.ViewModel.Templates
{
    public partial class MyEntryViewModel : ObservableObject
    {
        public MyEntryViewModel(string placeholder, string text, string icon,
            double fontScale = 1, Color entryBorderColor = null, double borderTicknessFocused = 2.6,
            double borderTicknessUnfocused = 1, DateTime date = default, bool isDate = false)
        {
            Placeholder = placeholder;
            Text = text;
            Icon = icon;
            FontSize = DeviceManager.Instance.EntryFontSize * fontScale;
            EntryBorderColor = entryBorderColor ?? Color.FromHex("#c8c8c8");
            BorderTicknessFocused = borderTicknessFocused;
            BorderTicknessUnfocused = borderTicknessUnfocused;
            Date = date == default ? DateTime.Today : date;
            IsDate = isDate;
            isText = !isDate;
        }

        public string Placeholder { get; set; }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, value);
            }
        }
        public string Icon { get; set; }

        public double FontSize { get; set; } = DeviceManager.Instance.EntryFontSize;
        public double IconSize { get; set; } = DeviceManager.Instance.DensityFactor * 32;

        [ObservableProperty]
        Color _entryBorderColor;

        [ObservableProperty]
        double _borderTicknessFocused, _borderTicknessUnfocused;

        [ObservableProperty]
        DateTime date = DateTime.Today;

        [ObservableProperty]
        bool isDate = false, isText = true;
    }
}
