namespace MuseoOmero.Managers
{
	public class DeviceManager
	{
		private static DeviceManager _instance;
		private DeviceManager() { }
		public static DeviceManager Instance
		{
			get
			{
				_instance ??= new DeviceManager();
				return _instance;
			}
		}

		private static ResourceDictionary _myColors = Application.Current.Resources.MergedDictionaries.First();
		public Color[] ColorsByRank { get; } = { (Color)_myColors["Primary"], (Color)_myColors["Secondary"], (Color)_myColors["Tertiary"] };
		public Color[] Colors { get; } = { (Color)_myColors["Color1"], (Color)_myColors["Color2"], (Color)_myColors["Color3"], (Color)_myColors["Color4"], (Color)_myColors["Color5"], (Color)_myColors["Color6"], (Color)_myColors["Color7"] };

		public double Width
		{
			get
			{
				return DeviceDisplay.MainDisplayInfo.Width / Density;
			}
		}
		public double Height
		{
			get => DeviceDisplay.MainDisplayInfo.Height / Density;
		}
		public double Density
		{
			get => DeviceDisplay.MainDisplayInfo.Density == 0 ? 1 : DeviceDisplay.MainDisplayInfo.Density;
		}

		public double DensityFactor
		{
			get
			{
#if ANDROID
                return 2.75 / Density;
#elif WINDOWS
                return 1 / Density;
#else
				return 1;
#endif
			}
		}


		public double EntryFontSize { get => DensityFactor * 19; }
		public double ButtonFontSize { get => DensityFactor * 18; }
	}
}
