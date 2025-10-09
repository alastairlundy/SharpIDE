using System.Threading.Channels;
using GDExtensionBindgen;
using Godot;

namespace SharpIDE.Godot.Features.Build;

public partial class BuildPanel : Control
{
    private Terminal _terminal = null!;
    private ChannelReader<string>? _buildOutputChannelReader;
    public override void _Ready()
    {
        _terminal = new Terminal(GetNode<Control>("%Terminal"));
        Singletons.BuildService.BuildStarted += OnBuildStarted;
    }

    public override void _Process(double delta)
    {
        if (_buildOutputChannelReader is null) return;
        // TODO: Buffer and write once per frame? Investigate if godot-xterm already buffers internally
        while (_buildOutputChannelReader.TryRead(out var str))
        {
            _terminal.Write(str);
        }
    }

    private async Task OnBuildStarted()
    {
        await this.InvokeAsync(() => _terminal.Clear());
        _buildOutputChannelReader ??= Singletons.BuildService.BuildTextWriter.ConsoleChannel.Reader;
    }
}