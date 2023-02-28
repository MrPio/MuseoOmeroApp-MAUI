﻿using System.Drawing;

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
	public FileStream CropImageToSquare(string path)
	{
		Bitmap bmpImage = new(System.Drawing.Image.FromFile(path));
		var width = bmpImage.Width;
		Bitmap bmpCropped;
		var height = bmpImage.Height;
		if (width > height)
			bmpCropped = bmpImage.Clone(new((width - height) / 2, 0, height, height), bmpImage.PixelFormat);
		else if (height > width)
			bmpCropped = bmpImage.Clone(new(0, (height - width) / 2, width, width), bmpImage.PixelFormat);
		else
			bmpCropped = bmpImage;

		MemoryStream memoryStream = new();
		var newPath = Path.Combine(FileSystem.AppDataDirectory, $"avatarCropped_{new Random().Next(999999)}.png");
		bmpCropped.Save(newPath);
		if (_stream is { })
			_stream.Close();
		_stream = File.OpenRead(newPath);
		return _stream;
	}
}