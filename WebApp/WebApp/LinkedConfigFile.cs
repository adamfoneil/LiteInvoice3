using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace WebApp;

public class LinkedConfigFile : IFileProvider
{
	public IDirectoryContents GetDirectoryContents(string subpath)
	{
		throw new NotImplementedException();
	}

	public IFileInfo GetFileInfo(string subpath)
	{
		var allJsonFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.json", SearchOption.AllDirectories);
		var fullPath = allJsonFiles.SingleOrDefault(path => path.EndsWith(subpath)) ?? throw new FileNotFoundException($"File not found: {subpath}");		
		throw new NotImplementedException();
	}

	public IChangeToken Watch(string filter)
	{
		throw new NotImplementedException();
	}
}
