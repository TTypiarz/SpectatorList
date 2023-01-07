using MEC;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerRoles.Spectating;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectatorList;

public class EventHandlers
{
    [PluginEvent(PluginAPI.Enums.ServerEventType.PlayerSpawn)]
    public void OnPlayerSpawn(Player player, RoleTypeId roleId) => Timing.RunCoroutine(SpectatorList(player).CancelWith(player.GameObject));

    private IEnumerator<float> SpectatorList(Player player)
    {
        while (player.IsAlive)
        {
            yield return Timing.WaitForSeconds(1);

            StringBuilder list = StringBuilderPool.Shared.Rent().Append(Plugin.Instance.Config.Title);

            int count = 0;
            foreach (Player splayer in Player.GetPlayers().Where(x => x.Role == PlayerRoles.RoleTypeId.Spectator))
            {
                if (Plugin.Instance.Config.Names.Contains("(NONE)")) break;

                if (((SpectatorRole)splayer.ReferenceHub.roleManager.CurrentRole).SyncedSpectatedNetId != player.NetworkId) continue;

                if (splayer.IsGlobalModerator ||
                    (splayer.Role == PlayerRoles.RoleTypeId.Overwatch && Plugin.Instance.Config.IgnoreOverwatch) ||
                    (splayer.IsNorthwoodStaff && Plugin.Instance.Config.IgnoreNorthwood) ||
                    Plugin.Instance.Config.IgnoredRoles.Contains(splayer.ReferenceHub.serverRoles.name))
                    continue;

                list.Append(Plugin.Instance.Config.Names.Replace("(NAME)", splayer.Nickname));
                count++;
            }

            if (count > 0)
            {
                string spectatorList = StringBuilderPool.Shared.ToStringReturn(list)
                        .Replace("(COUNT)", count.ToString())
                        .Replace("(COLOR)", player.ReferenceHub.roleManager.CurrentRole.RoleColor.ToHex()
                        .Replace("<br>", "\n"));

                player.ReceiveHint(spectatorList, 2f);
            }
            else StringBuilderPool.Shared.Return(list);
        }
    }
}