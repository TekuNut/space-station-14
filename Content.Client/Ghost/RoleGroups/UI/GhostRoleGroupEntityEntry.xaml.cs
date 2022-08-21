using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.Ghost.RoleGroups.UI;

[GenerateTypedNameReferences]
public sealed partial class GhostRoleGroupEntityEntry : BoxContainer
{
    public event Action<EntityUid>? OnEntityGoto;

    public GhostRoleGroupEntityEntry(string name, EntityUid entity)
    {
        RobustXamlLoader.Load(this);

        EntityNameLabel.Text = name;
        GotoButton.OnPressed += _ => OnEntityGoto?.Invoke(entity);
    }
}