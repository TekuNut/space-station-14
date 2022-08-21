using Content.Server.Administration.Managers;
using Content.Server.EUI;
using Content.Shared.Eui;
using Content.Shared.Ghost.Roles;

namespace Content.Server.Ghost.Roles.UI
{
    public sealed class GhostRolesEui : BaseEui
    {
        public override GhostRolesEuiState GetNewState()
        {
            var manager = EntitySystem.Get<GhostRoleSelectionSystem>();
            var adminManager = IoCManager.Resolve<IAdminManager>();

            return new GhostRolesEuiState(
                manager.GetGhostRoleGroupsInfo(),
                manager.GetGhostRolesInfo(),
                manager.GetPlayerRequestedGhostRoles(Player),
                manager.GetPlayerRequestedRoleGroups(Player),
                manager.LotteryStartTime,
                manager.LotteryExpiresTime,
                adminManager.IsAdmin(Player));
        }

        public override void HandleMessage(EuiMessageBase msg)
        {
            base.HandleMessage(msg);

            switch (msg)
            {
                case GhostRoleTakeoverRequestMessage req:
                    EntitySystem.Get<GhostRoleSelectionSystem>().RequestTakeover(Player, req.Identifier);
                    break;
                case GhostRoleFollowRequestMessage req:
                    EntitySystem.Get<GhostRoleSelectionSystem>().Follow(Player, req.Identifier);
                    break;
                case GhostRoleLotteryRequestMessage req:
                    EntitySystem.Get<GhostRoleSelectionSystem>().GhostRoleAddPlayerLotteryRequest(Player, req.Identifier);
                    break;
                case GhostRoleCancelLotteryRequestMessage req:
                    EntitySystem.Get<GhostRoleSelectionSystem>().GhostRoleRemovePlayerLotteryRequest(Player, req.Identifier);
                    break;
                case GhostRoleGroupLotteryRequestMessage req:
                    EntitySystem.Get<GhostRoleSelectionSystem>().GhostRoleGroupAddPlayerLotteryRequest(Player, req.Identifier);
                    break;
                case GhostRoleGroupCancelLotteryMessage req:
                    EntitySystem.Get<GhostRoleSelectionSystem>().GhostRoleGroupRemovePlayerLotteryRequest(Player, req.Identifier);
                    break;
                case GhostRoleWindowCloseMessage _:
                    Closed();
                    break;
            }
        }

        public override void Closed()
        {
            base.Closed();

            EntitySystem.Get<GhostRoleSelectionSystem>().CloseEui(Player);
        }
    }
}
