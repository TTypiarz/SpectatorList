using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using NorthwoodLib.Pools;
using PlayerRoles.Spectating;
using System.Collections.Generic;
using System.Text;

namespace SpectatorList;

public class EventHandlers
{
    private readonly Plugin plugin;
    public EventHandlers(Plugin plugin) => this.plugin = plugin;

    public void OnSpawned(SpawnedEventArgs ev) => Timing.RunCoroutine(SpectatorList(ev.Player).CancelWith(ev.Player.GameObject));

    private IEnumerator<float> SpectatorList(Player player)
    {
        while (player.IsAlive)
        {
            yield return Timing.WaitForSeconds(1);

            StringBuilder list = StringBuilderPool.Shared.Rent().Append(plugin.Translation.Title);

            int count = 0;
            foreach (Player splayer in player.CurrentSpectatingPlayers)
            {
                if (plugin.Translation.Names.Contains("(NONE)")) break;

                if (((SpectatorRole)splayer.RoleManager.CurrentRole).SyncedSpectatedNetId != player.NetworkIdentity.netId) continue;

                if (splayer.IsGlobalModerator ||
                    (splayer.IsOverwatchEnabled && plugin.Config.IgnoreOverwatch) ||
                    (splayer.IsNorthwoodStaff && plugin.Config.IgnoreNorthwood) ||
                    plugin.Config.IgnoredRoles.Contains(splayer.ReferenceHub.serverRoles.name))
                    continue;

                list.Append(plugin.Translation.Names.Replace("(NAME)", splayer.Nickname));
                count++;
            }

            if (count == 0) StringBuilderPool.Shared.Return(list);

            string spectatorList = StringBuilderPool.Shared.ToStringReturn(list)
                    .Replace("(COUNT)", count.ToString())
                    .Replace("(COLOR)", player.ReferenceHub.roleManager.CurrentRole.RoleColor.ToHex()
                    .Replace("<br>", "\n"));

            player.ShowHint(spectatorList, 2f);
        }
    }
}