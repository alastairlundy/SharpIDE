using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Roslyn.Benchmarks;

public class MSBuildWorkspaceBenchmarks
{
	private const string _solutionFilePath = "C:/Users/Matthew/Documents/Git/StatusApp/StatusApp.sln";
	
	// | Method                    | Mean     | Error    | StdDev   |
	// |-------------------------- |---------:|---------:|---------:|
	// | CreateWorkspaceNoParams   | 10.88 us | 0.045 us | 0.042 us |
	// | CreateWorkspaceWithParams | 11.22 us | 0.072 us | 0.060 us |
	[Benchmark]
	public MSBuildWorkspace CreateWorkspaceNoParams()
	{
		var workspace = MSBuildWorkspace.Create();
		return workspace;
	}

	[Benchmark]
	public MSBuildWorkspace CreateWorkspaceWithParams()
	{
		var properties = new Dictionary<string, string>
		{
			{ "Configuration", "Debug" },
			{ "Platform", "AnyCPU" }
		};
		var workspace = MSBuildWorkspace.Create(properties);
		return workspace;
	}

	// [Benchmark]
	// public async Task<Solution> ParseSolutionFileFromPath()
	// {
	// 	var workspace = MSBuildWorkspace.Create();
	// 	var solution = await workspace.OpenSolutionAsync(_solutionFilePath);
	// 	return solution;
	// }
}
