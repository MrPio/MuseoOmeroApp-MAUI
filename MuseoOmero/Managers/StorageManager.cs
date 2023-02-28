using Firebase.Auth;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;

namespace MuseoOmero.Managers;

class StorageManager
{
	private static StorageManager _instance;
	private StorageManager() { }
	public static StorageManager Instance
	{
		get
		{
			_instance ??= new StorageManager();
			return _instance;
		}
	}

	public FirebaseStorage FirebaseStorage = new(
		storageBucket: "museoomero-ca8aa.appspot.com"
	);

	private FirebaseStorageReference GetChild(string resource)
	{
		FirebaseStorageReference fc = null;
		foreach (var child in resource.Split('/', StringSplitOptions.RemoveEmptyEntries))
			fc = fc is null ? FirebaseStorage.Child(child) : fc.Child(child);
		return fc;
	}

	public async Task Upload(string resource, Stream stream, EventHandler<FirebaseStorageProgress> callback = null)
	{
		var task = GetChild(resource).PutAsync(stream);
		if (callback is { })
			task.Progress.ProgressChanged += callback;
		await task;
	}

	public async Task<string> GetLink(string resource)
	{
		try
		{
			return await GetChild(resource).GetDownloadUrlAsync();
		}
		catch (FirebaseStorageException e)
		{
			return null;
		}
	}
}
