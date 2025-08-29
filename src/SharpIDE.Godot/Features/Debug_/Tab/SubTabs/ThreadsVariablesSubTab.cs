using Godot;

namespace SharpIDE.Godot.Features.Debug_.Tab.SubTabs;

public partial class ThreadsVariablesSubTab : Control
{
    private VBoxContainer _threadsVboxContainer = null!;

    public override void _Ready()
    {
        _threadsVboxContainer = GetNode<VBoxContainer>("%ThreadsPanel/VBoxContainer");
    }
}