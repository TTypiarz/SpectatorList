using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Text;

namespace SpectatorList
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnVerified(VerifiedEventArgs ev) =>
            Timing.RunCoroutine(SpectatorList(ev.Player).CancelWith(ev.Player.GameObject));

        private IEnumerator<float> SpectatorList(Player player)
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1);
                yield return Timing.WaitUntilTrue(() => player.IsAlive);

                StringBuilder list = StringBuilderPool.Shared.Rent();
                list.Append(plugin.Translation.Title);

                int count = 0;

                foreach (Player splayer in player.CurrentSpectatingPlayers)
                {
                    if (splayer == player || splayer.IsGlobalModerator ||
                        splayer.IsOverwatchEnabled && plugin.Config.IgnoreOverwatch ||
                        splayer.IsNorthwoodStaff && plugin.Config.IgnoreNorthwood) continue;

                    if (!plugin.Translation.Names.Equals("(none)", StringComparison.Ordinal))
                        list.Append(plugin.Translation.Names.Replace("(NAME)", splayer.Nickname));

                    count++;
                }

                if (count > 0)
                    player.ShowHint(
                        StringBuilderPool.Shared.ToStringReturn(list).Replace("(COUNT)", $"{count}")
                            .Replace("(COLOR)", player.Role.Color.ToHex()), 1.2f);
                else StringBuilderPool.Shared.Return(list);
            }
        }
    }
}