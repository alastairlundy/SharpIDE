using GDExtensionBindgen;
using Godot;
using SharpIDE.Application.Features.SolutionDiscovery.VsPersistence;

namespace SharpIDE.Godot.Features.Run;

public partial class RunPanelTab : Control
{
	private Terminal _terminal = null!;
    
    public SharpIdeProjectModel Project { get; set; } = null!;
    public int TabBarTab { get; set; }

    public override void _Ready()
    {
		_terminal = new Terminal();
		AddChild(_terminal);
    }
    
    public void ClearTerminal()
	{
		_terminal.Clear();
	}
}