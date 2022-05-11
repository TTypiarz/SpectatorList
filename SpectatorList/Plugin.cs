using MEC;
using NorthwoodLib.Pools;
using PluginAPI.Core;
using PluginAPI.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpectatorList
{
    public class Plugin : Plugin<PluginConfig>
    {
        public override string Name { get; } = "SpectatorList";
        public override string Author { get; } = "TTypiarz";
        public override Version Version { get; } = new Version(1, 1, 3);

        public override void OnEnabled()
        {
            Player.Joined += Player_Joined;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Joined -= Player_Joined;

            base.OnDisabled();
        }

        private void Player_Joined(PlayerJoinEvent ev) =>
    Timing.RunCoroutine(SpectatorList(ev.Player).CancelWith(ev.Player.GameObject));

        private IEnumerator<float> SpectatorList(Player player)
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1);
                yield return Timing.WaitUntilTrue(() => player.IsAlive);

                StringBuilder list = StringBuilderPool.Shared.Rent();
                list.Append(Config.Title);

                int count = 0;

                foreach (ReferenceHub refhub in player.ReferenceHub.spectatorManager.ServerCurrentSpectatingPlayers)
                {
                    Player splayer = Player.Get(refhub.gameObject);

                    if (splayer == player || splayer.IsGlobalModerator ||
                        splayer.Overwatch && Config.IgnoreOverwatch ||
                        splayer.IsNorthwoodStaff && Config.IgnoreNorthwood) continue;

                    if (!Config.Names.Equals("(none)", StringComparison.Ordinal))
                        list.Append(Config.Names.Replace("(NAME)", splayer.Nickname));

                    count++;
                }

                if (count > 0)
                    player.ShowHint(
                        StringBuilderPool.Shared.ToStringReturn(list).Replace("(COUNT)", $"{count}")
                            .Replace("(COLOR)", $"{CharacterClassManager._staticClasses.Get(player.Role).classColor.ToHex()}"), 1.2f);
                else StringBuilderPool.Shared.Return(list);
            }
        }
    }
}