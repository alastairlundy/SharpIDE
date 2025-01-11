using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Roslyn.Benchmarks;

public class ParseSolutionBenchmarks
{
	private const string _solutionFilePath = "C:/Users/Matthew/Documents/Git/StatusApp/StatusApp.sln";


	//[IterationSetup]

	[Benchmark]
	public async Task<Solution> ParseSolutionFileFromPath()
	{
		var workspace = MSBuildWorkspace.Create();
		var solution = await workspace.OpenSolutionAsync(_solutionFilePath);
		return solution;
	}
}
