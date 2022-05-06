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
        private readonly Config _config = Plugin.Singleton.Config;
        private readonly Translation _translation = Plugin.Singleton.Translation;
        
        public void OnVerified(VerifiedEventArgs ev) => Timing.RunCoroutine(SpectatorList(ev.Player).CancelWith(ev.Player.GameObject));

        private IEnumerator<float> SpectatorList(Player player)
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1);
                yield return Timing.WaitUntilTrue(() => player.IsAlive);
                
                StringBuilder list = StringBuilderPool.Shared.Rent();
                int count = 0;

                list.Append(_translation.Title);
                
                foreach (Player splayer in player.CurrentSpectatingPlayers)
                {
                    if (splayer != player && !splayer.IsGlobalModerator && !(splayer.IsOverwatchEnabled && _config.IgnoreOverwatch) && !(splayer.IsNorthwoodStaff && _config.IgnoreNorthwood))
                    {
                        if (!_translation.Names.Equals("(none)"))
                            list.Append(_translation.Names.Replace("(NAME)", splayer.Nickname));

                        count++;
                    }
                }

                if (count > 0)
                    player.ShowHint(StringBuilderPool.Shared.ToStringReturn(list).Replace("(COUNT)", $"{count}").Replace("(COLOR)", player.Role.Color.ToHex()), 1.2f);
                else StringBuilderPool.Shared.Return(list);
            }
        }
    }
}
