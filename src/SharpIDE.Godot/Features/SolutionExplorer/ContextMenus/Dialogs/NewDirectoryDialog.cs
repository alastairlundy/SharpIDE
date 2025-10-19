using Godot;
using SharpIDE.Application.Features.SolutionDiscovery;

namespace SharpIDE.Godot.Features.SolutionExplorer.ContextMenus.Dialogs;

public partial class NewDirectoryDialog : ConfirmationDialog
{
    private LineEdit _nameLineEdit = null!;
    
    public SharpIdeFolder ParentFolder { get; set; } = null!;

    public override void _Ready()
    {
        _nameLineEdit = GetNode<LineEdit>("%DirectoryNameLineEdit");
        _nameLineEdit.GrabFocus();
        _nameLineEdit.SelectAll();
        Confirmed += OnConfirmed;
    }

    private void OnConfirmed()
    {
        var directoryName = _nameLineEdit.Text.Trim();
    }
}