using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectatorList
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnVerified(VerifiedEventArgs ev)
        {
            Timing.RunCoroutine(SpectatorList(ev.Player));
        }

        public IEnumerator<float> SpectatorList(Player player)
        {
            yield return Timing.WaitForSeconds(1);
            while (true)
            {
                yield return Timing.WaitForSeconds(1);

                yield return Timing.WaitUntilTrue(() => player.IsAlive);
                StringBuilder list = StringBuilderPool.Shared.Rent();
                int count = 0;

                if (player.CurrentSpectatingPlayers.Count() == 1)
                {
                    list.Append("<align=right><size=45%><color=orange><b>Spectators ({COUNT}):</b>");
                    foreach (Player splayer in player.CurrentSpectatingPlayers)
                    {
                        if (splayer != player && ((splayer.IsOverwatchEnabled && !plugin.Config.IgnoreOverwatch) || (splayer.IsNorthwoodStaff && !plugin.Config.IgnoreNorthwood) || !splayer.IsGlobalModerator))
                        {
                            list.Append($"\n{splayer.Nickname}");
                            count++;
                        }
                    }
                    list.Append("</color></size></align>");

                    player.ShowHint(list.ToString().Replace("{COUNT}", $"{count}"), 1.2f);
                }
            }
        }
    }
}
