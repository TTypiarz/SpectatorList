using Exiled.API.Features;
using NorthwoodLib.Pools;
using PlayerRoles.Spectating;
using System.Linq;
using System.Text;

namespace SpectatorList;

// With love by Jesus-QC <3
public class CustomHintDisplay
{
    private readonly StringBuilder _stringBuilder = StringBuilderPool.Shared.Rent();

    public void Clear() => StringBuilderPool.Shared.Return(_stringBuilder);

    public string Draw(Player player, string hint)
    {
        _stringBuilder.Clear();
        _stringBuilder.AppendLine("<align=right><size=45%><color=" + player.Role.Color.ToHex() + '>' + EntryPoint.Instance.SpectatorListConfig.SpectatorListTitle);

        int count = 0;
        foreach (Player spectator in Player.List)
        {
            if (EntryPoint.Instance.SpectatorListConfig.SpectatorNames.Contains("(NONE)"))
                break;

            if (spectator.ReferenceHub.roleManager.CurrentRole is not SpectatorRole spectatorRole || spectatorRole.SyncedSpectatedNetId != player.NetworkIdentity.netId)
                continue;

            if (spectator.IsGlobalModerator || spectator.IsOverwatchEnabled && EntryPoint.Instance.SpectatorListConfig.IgnoreOverwatch || spectator.IsNorthwoodStaff && EntryPoint.Instance.SpectatorListConfig.IgnoreNorthwood)
                continue;

            if (EntryPoint.Instance.SpectatorListConfig.IgnoredRoles.Contains(spectator.ReferenceHub.serverRoles.GetUncoloredRoleString()))
                continue;

            _stringBuilder.AppendLine(EntryPoint.Instance.SpectatorListConfig.SpectatorNames.Replace("(NAME)", spectator.Nickname));
            count++;
        }

        for (int i = count; i < 30; i++)
        {
            _stringBuilder.AppendLine();
        }

        _stringBuilder.Append("</color></size></align>");

        _stringBuilder.AppendLine(FillMissingLines(hint));
        _stringBuilder.AppendLine("<size=700%>\n</size>");
        return _stringBuilder.ToString().Replace("(COUNT)", count.ToString());
    }

    private static string FillMissingLines(string text, int linesNeeded = 6)
    {
        int textLines = text.Count(x => x == '\n');

        for (int i = 0; i < linesNeeded - textLines; i++)
            text += '\n';

        return text;
    }
}