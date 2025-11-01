namespace SharpIDE.Application.Features.Nuget;

public enum NugetPackageIconFormat
{
	Png,
	Jpg
}
public class NugetPackageIconCacheService(IHttpClientFactory httpClientFactory)
{
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

	// TODO: Add an in memory cache
	public async Task<(byte[]? bytes, NugetPackageIconFormat?)> GetNugetPackageIcon(string packageId, Uri? iconUrl)
	{
		var appdataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		var cacheFolder = Path.Combine(appdataFolderPath, "SharpIDE", "NugetPackageIconCache");
		Directory.CreateDirectory(cacheFolder);
		var packageIconFilePath = Path.Combine(cacheFolder, $"{packageId}.bin");
		if (File.Exists(packageIconFilePath))
		{
			var bytes = await File.ReadAllBytesAsync(packageIconFilePath);
			return (bytes, GetImageFormat(bytes));
		}
		else if (iconUrl is null)
		{
			return (null, null);
		}
		else
		{
			var httpClient = _httpClientFactory.CreateClient();
			var iconBytes = await httpClient.GetByteArrayAsync(iconUrl);
			await File.WriteAllBytesAsync(packageIconFilePath, iconBytes);
			return (iconBytes, GetImageFormat(iconBytes));
		}
	}

	private static NugetPackageIconFormat? GetImageFormat(byte[] imageBytes)
	{
		// PNG files start with 89 50 4E 47 0D 0A 1A 0A
		if (imageBytes is [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, ..])
		{
			return NugetPackageIconFormat.Png;
		}

		// JPEG files start with FF D8 and end with FF D9
		if (imageBytes is [0xFF, 0xD8, .., 0xFF, 0xD9])
		{
			return NugetPackageIconFormat.Jpg;
		}

		return null;
	}
}
