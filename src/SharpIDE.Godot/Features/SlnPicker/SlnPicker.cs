using Godot;

namespace SharpIDE.Godot.Features.SlnPicker;

public partial class SlnPicker : Control
{
    private FileDialog _fileDialog = null!;
    private Button _openSlnButton = null!;

    private readonly TaskCompletionSource<string?> _tcs = new TaskCompletionSource<string?>(TaskCreationOptions.RunContinuationsAsynchronously);

    public override void _Ready()
    {
        _fileDialog = GetNode<FileDialog>("%FileDialog");
        _openSlnButton = GetNode<Button>("%OpenSlnButton");
        _openSlnButton.Pressed += () => _fileDialog.PopupCentered();
        var windowParent = GetParentOrNull<Window>();
        _fileDialog.FileSelected += path =>
        {
            _tcs.SetResult(path);
            windowParent?.Hide();
        };
        _fileDialog.Canceled += () => _tcs.SetResult(null);
        windowParent?.CloseRequested += OnCloseRequested;
    }

    private void OnCloseRequested()
    {
        _tcs.SetResult(null);
    }

    public async Task<string?> GetSelectedSolutionPath()
    {
        return await _tcs.Task;
    }
}
