using Content.Shared.Ghost.Roles;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.Ghost.RoleGroups.UI;

[GenerateTypedNameReferences]
public sealed partial class AdminGhostRoleGroupsWindow : DefaultWindow
{
    public event Action<AdminGhostRoleGroupInfo>? OnGroupActivate;
    public event Action<AdminGhostRoleGroupInfo>? OnGroupRelease;
    public event Action<AdminGhostRoleGroupInfo>? OnGroupDelete;
    public event Action<EntityUid>? OnEntityGoto;
    public event Action? OnGroupStart;

    private readonly HashSet<uint> _ghostRoleGroupOpenedDetails = new();

    public AdminGhostRoleGroupsWindow()
    {
        RobustXamlLoader.Load(this);
        StartGroupButton.OnPressed += _ => OnGroupStart?.Invoke();
    }

    public void ClearEntries()
    {
        NoRoleGroupsMessage.Visible = true;
        EntryContainer.Children.Clear();
    }

    public void AddEntry(AdminGhostRoleGroupInfo group, IReadOnlyDictionary<EntityUid, string> entityNames)
    {
        NoRoleGroupsMessage.Visible = false;

        var entry = new GhostRoleGroupAdminEntry(group, entityNames);

        entry.OnGroupActivate += OnGroupActivate;
        entry.OnGroupRelease += OnGroupRelease;
        entry.OnGroupDelete += OnGroupDelete;
        entry.OnEntityGoto += OnEntityGoto;
        entry.OnGroupShowDetails += roleGroup =>
        {
            if (!_ghostRoleGroupOpenedDetails.Remove(group.GroupIdentifier))
            {
                entry.ShowDetails(true);
                _ghostRoleGroupOpenedDetails.Add(group.GroupIdentifier);
                return;
            }

            entry.ShowDetails(false);
        };

        entry.ShowDetails(_ghostRoleGroupOpenedDetails.Contains(group.GroupIdentifier));
        EntryContainer.AddChild(entry);
    }
}