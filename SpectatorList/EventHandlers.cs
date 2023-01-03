using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Text;

namespace SpectatorList;

public class EventHandlers
{
    private readonly Plugin plugin;
    public EventHandlers(Plugin plugin) => this.plugin = plugin;

    public void OnVerified(VerifiedEventArgs ev) => Timing.RunCoroutine(SpectatorList(ev.Player).CancelWith(ev.Player.GameObject));

    private IEnumerator<float> SpectatorList(Player player)
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(1);
            yield return Timing.WaitUntilTrue(() => player.IsAlive);

            StringBuilder list = StringBuilderPool.Shared.Rent().Append(plugin.Translation.Title);

            int count = 0;
            foreach (Player splayer in player.CurrentSpectatingPlayers)
            {
                Log.Debug($"{splayer.Nickname} is Spectating {player.Nickname}");
                if (splayer.IsGlobalModerator ||
                    splayer.IsOverwatchEnabled && plugin.Config.IgnoreOverwatch ||
                    splayer.IsNorthwoodStaff && plugin.Config.IgnoreNorthwood ||
                    plugin.Config.IgnoredRoles.Contains(splayer.GroupName) && !plugin.Config.IgnoredRoles.IsEmpty())
                    continue;

                if (plugin.Translation.Names.Contains("(NONE)")) continue;

                list.Append(plugin.Translation.Names.Replace("(NAME)", splayer.Nickname));
                count++;
            }

            if (count > 0)
            {
                string spectatorList = StringBuilderPool.Shared.ToStringReturn(list)
                        .Replace("(COUNT)", count.ToString())
                        .Replace("(COLOR)", player.Role.Color.ToHex());

                Log.Debug(spectatorList);
                player.ShowHint(spectatorList, 1.2f);
            }
            else StringBuilderPool.Shared.Return(list);
        }
    }
}