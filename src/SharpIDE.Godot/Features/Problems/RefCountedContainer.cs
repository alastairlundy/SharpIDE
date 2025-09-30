using Godot;
using Microsoft.CodeAnalysis;
using SharpIDE.Application.Features.SolutionDiscovery.VsPersistence;

namespace SharpIDE.Godot.Features.Problems;

public partial class RefCountedContainer<T>(T item) : RefCounted
{
    public T Item { get; } = item;
}
