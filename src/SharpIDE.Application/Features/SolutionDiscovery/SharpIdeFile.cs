using SharpIDE.Application.Features.SolutionDiscovery.VsPersistence;

namespace SharpIDE.Application.Features.SolutionDiscovery;

public class SharpIdeFile : ISharpIdeNode
{
	public required string Path { get; set; }
	public required string Name { get; set; }
}
