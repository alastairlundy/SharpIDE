using SharpIDE.Application.Features.Debugging;
using SharpIDE.Application.Features.SolutionDiscovery.VsPersistence;

namespace SharpIDE.Application.Features.Events;

public class GlobalEvents
{
	public static GlobalEvents Instance { get; set; } = null!;
	public EventWrapper<Task> ProjectsRunningChanged { get; private set; } = new(() => Task.CompletedTask);
	public EventWrapper<Task> StartedRunningProject { get; private set; } = new(() => Task.CompletedTask);
	public EventWrapper<SharpIdeProjectModel, Task> ProjectStartedDebugging { get; private set; } = new(_ => Task.CompletedTask);
	public EventWrapper<SharpIdeProjectModel, Task> ProjectStoppedDebugging { get; private set; } = new(_ => Task.CompletedTask);
	public EventWrapper<SharpIdeProjectModel, Task> ProjectStartedRunning { get; private set; } = new(_ => Task.CompletedTask);
	public EventWrapper<SharpIdeProjectModel, Task> ProjectStoppedRunning { get; private set; } = new(_ => Task.CompletedTask);
	public EventWrapper<ExecutionStopInfo, Task> DebuggerExecutionStopped { get; private set; } = new(_ => Task.CompletedTask);
}
