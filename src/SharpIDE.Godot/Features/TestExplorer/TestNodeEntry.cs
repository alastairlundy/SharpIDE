using Godot;
using SharpIDE.Application.Features.Testing.Client;
using SharpIDE.Application.Features.Testing.Client.Dtos;

namespace SharpIDE.Godot.Features.TestExplorer;

public partial class TestNodeEntry : MarginContainer
{
    private Label _testNameLabel = null!;
    private Label _testNodeStatusLabel = null!;

    public TestNode TestNode { get; set; } = null!;
    private static readonly Color SuccessTextColour = new Color("499c54");
    private static readonly Color RunningTextColour = new Color("a77fd2");
    private static readonly Color PendingTextColour = new Color("2aa9e7");
    private static readonly Color FailedTextColour = new Color("c65344");
    private static readonly Color CancelledTextColour = new Color("e4a631");
    private static readonly Color SkippedTextColour = new Color("c0c0c0");

    public override void _Ready()
    {
        _testNameLabel = GetNode<Label>("%TestNameLabel");
        _testNodeStatusLabel = GetNode<Label>("%TestNodeStatusLabel");
        _testNameLabel.Text = string.Empty;
        _testNodeStatusLabel.Text = string.Empty;
        SetValues();
    }

    public void SetValues()
    {
        if (TestNode == null) return;
        _testNameLabel.Text = TestNode.DisplayName;
        _testNodeStatusLabel.Text = TestNode.ExecutionState;
        _testNodeStatusLabel.AddThemeColorOverride(ThemeStringNames.FontColor, GetTextColour(TestNode.ExecutionState));
    }

    private static Color GetTextColour(string executionState)
    {
        var colour = executionState switch
        {
            ExecutionStates.Passed => SuccessTextColour,
            ExecutionStates.InProgress => RunningTextColour,
            ExecutionStates.Discovered => PendingTextColour,
            ExecutionStates.Failed => FailedTextColour,
            ExecutionStates.Cancelled => CancelledTextColour,
            ExecutionStates.Skipped => SkippedTextColour,
            _ => Colors.White,
        };
        return colour;
    }
}