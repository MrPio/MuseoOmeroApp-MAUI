using System.Drawing;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.ZKWeb;
using System.Linq;

namespace MuseoOmero.ViewModelWin;
public partial class BiglietteriaViewModelWin : ObservableObject
{
	private static readonly string GalleryFolder = FileSystem.AppDataDirectory;

	private readonly IMediaPicker _mediaPicker;
	public List<Biglietto> bigliettiTotali = new();

	[ObservableProperty]
	ObservableCollection<Biglietto> convalidatiOggi = new(), bigliettiOrdinati = new();

	[ObservableProperty]
	bool noConvalideOggi = true, isBusy = false, bigliettiSortAcending = true;

	[ObservableProperty]
	HomeViewModelWin homeViewModelWin;

	[ObservableProperty]
	Biglietto selectedBiglietto;

	[ObservableProperty]
	string filtroBiglietti = "DataValid";



	[RelayCommand]
	async Task CapturePhoto()
	{
		if (!MediaPicker.Default.IsCaptureSupported)
		{
			await Shell.Current.DisplayAlert("Hardware non disponibile", "Spiacente, ma il tuo dispositivo non supporta questa funzione", "Ok");
			return;
		}
		var options = new MediaPickerOptions() { Title = "Avvicina il codice QR del biglietto da convalidare" };
		try
		{
			IsBusy = true;
			FileResult file = await _mediaPicker.CapturePhotoAsync();
			if (file is { })
			{
				string localFilePath = Path.Combine(GalleryFolder, "photo.jpg");
				using FileStream localFileStream = File.OpenWrite(localFilePath);
#if WINDOWS
				// on Windows file.OpenReadAsync() throws an exception
				using Stream sourceStream = File.OpenRead(file.FullPath);
#else
				using Stream sourceStream = await file.OpenReadAsync();
#endif
				await sourceStream.CopyToAsync(localFileStream);

				localFileStream.Close();
				sourceStream.Close();
				var result = ScanQrCode(localFilePath);

				if (result is null)
				{
					await Shell.Current.DisplayAlert("Riprova", "Spiacente, nessun codice QR trovato nell'immagine, si prega di riprovare.", "Ok");
				}
				else
				{
					await ConvalidaBiglietto(result);
				}
			}
		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Errore", "Spiacente, si è riscontrato un errore generico", "Ok");
		}
		finally
		{
			IsBusy = false;
		}
	}
	[RelayCommand]
	void HeaderLabelTap(string titolo)
	{
		if (FiltroBiglietti == titolo)
			BigliettiSortAcending = !BigliettiSortAcending;
		else
			BigliettiSortAcending = true;
		FiltroBiglietti = titolo;
		OrdinaBiglietti();
	}
	[RelayCommand]
	void InvertSortTap()
	{
		BigliettiSortAcending = !BigliettiSortAcending;
		OrdinaBiglietti();
	}

	public void OrdinaBiglietti()
	{
		Func<Biglietto, dynamic> opereFunc = FiltroBiglietti switch
		{
			"DataValid" => o => o.DataValidita,
			"Tipologia" => o => o.Tipologia.ToString(),
			"DataAcq" => o => o.DataAcquisto,
			"DataConv" => o => o.DataConvalida,
			_ => o => o.DataValidita
		};
		var bigliettiOrdinati = bigliettiTotali.OrderBy(opereFunc).ToList();
		if (!BigliettiSortAcending)
			bigliettiOrdinati.Reverse();
		BigliettiOrdinati = new ObservableCollection<Biglietto>(bigliettiOrdinati.ToList());
	}
	string ScanQrCode(string path)
	{
		System.Drawing.Image image;
		int width, height;
		using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
		{
			using var imageStream = System.Drawing.Image.FromStream(fileStream, false, false);
			image = imageStream;
			height = image.Height;
			width = image.Width;
		}

		var bin = File.ReadAllBytes(path);
		//make a bmp from the selected file
		using (var stream = new MemoryStream(bin))
		using (var bmp = new System.DrawingCore.Bitmap(stream))
		{
			var source = new BitmapLuminanceSource(bmp);
			var bitmap = new BinaryBitmap(new HybridBinarizer(source));
			var result = new MultiFormatReader().decode(bitmap);
			return result is null ? null : result.Text;
		}
	}

	public BiglietteriaViewModelWin(HomeViewModelWin homeViewModelWin, IMediaPicker mediaPicker)
	{
		HomeViewModelWin = homeViewModelWin;
		_mediaPicker = mediaPicker;
	}

	public void Initialize()
	{
		foreach (var b in HomeViewModelWin.Utenti.SelectMany(u => u.Biglietti))
		{
			if (b.DataConvalida is { } && b.DataConvalida?.Date == DateTime.Today)
			{
				ConvalidatiOggi.Add(b);
				NoConvalideOggi = false;
			}

			bigliettiTotali.Add(b);
			OrdinaBiglietti();
		}
	}

	async Task ConvalidaBiglietto(string id)
	{
		foreach (var u in HomeViewModelWin.Utenti)
		{
			var count = 0;
			foreach (var b in u.Biglietti)
			{
				if (b.Uid == id)
				{
					if(b.DataValidita.Date.Subtract(DateTime.Today).TotalHours<1)
					{
						await Shell.Current.DisplayAlert("Biglietto Scaduto", $"Il biglietto scansionato è scaduto in data {b.DataValidita:d MMM yyyy}", "Ok");
						return;
					}
					else if (b.DataValidita.Date.Subtract(DateTime.Today).TotalHours > 24)
					{
						await Shell.Current.DisplayAlert("Biglietto Invalido", $"Il biglietto scansionato non è valido oggi, può essere convalidato in data {b.DataValidita:d MMM yyyy}", "Ok");
						return;
					}
					b.DataConvalida = DateTime.Now;
					ConvalidatiOggi.Add(b);
					NoConvalideOggi = false;
					await DatabaseManager.Instance.Put($"utenti/{u.Uid}/biglietti/{count}", b);
					++count;
				}
			}
		}
	}

}
