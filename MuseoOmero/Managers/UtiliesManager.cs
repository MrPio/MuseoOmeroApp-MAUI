using System.DrawingCore;

namespace MuseoOmero.Managers;

public class UtiliesManager
{
	private static UtiliesManager _instance;
	private UtiliesManager() { }
	public static UtiliesManager Instance
	{
		get
		{
			_instance ??= new UtiliesManager();
			return _instance;
		}
	}
	private FileStream _stream;
	public FileStream ImageToStream(string path, bool crop)
	{
		System.Drawing.Bitmap bmpImage = new(System.Drawing.Image.FromFile(path));
		System.Drawing.Bitmap bmpCropped;
		if (crop)
		{
			var width = bmpImage.Width;
			var height = bmpImage.Height;
			if (width > height)
				bmpCropped = bmpImage.Clone(new((width - height) / 2, 0, height, height), bmpImage.PixelFormat);
			else if (height > width)
				bmpCropped = bmpImage.Clone(new(0, (height - width) / 2, width, width), bmpImage.PixelFormat);
			else
				bmpCropped = bmpImage;
		}
		else
		{
			bmpCropped = bmpImage;
		}

		MemoryStream memoryStream = new();
		var newPath = Path.Combine(FileSystem.AppDataDirectory, $"avatarCropped_{new Random().Next(999999)}.png");
		bmpCropped.Save(newPath);
		if (_stream is { })
			_stream.Close();
		_stream = File.OpenRead(newPath);
		return _stream;
	}


	public static Random Random = new();
	public string RandomString(int length)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuwxyz0123456789";
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[Random.Next(s.Length)]).ToArray());
	}

	public string RandomStringNumeric(int length)
	{
		const string chars = "0123456789";
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[Random.Next(s.Length)]).ToArray());
	}

	public DateTime RandomDate(int dayBack, int dayForward = 0)
	{
		dayBack = Math.Abs(dayBack);
		dayBack -= 1;
		int daysShift = Random.Next(dayBack + dayForward) - dayBack;
		int secondsShift = Random.Next(3600 * 24);
		return DateTime.Now.AddDays(daysShift).AddSeconds(-secondsShift);
	}
}
