using NorthwoodLib.Pools;
using PlayerRoles.Spectating;
using PluginAPI.Core;
using System.Linq;
using System.Text;

namespace SpectatorList;

// With love by Jesus-QC <3
public class CustomHintDisplay
{
    private readonly StringBuilder _stringBuilder = StringBuilderPool.Shared.Rent();

    public void Clear() => _stringBuilder.Clear();

    public string Draw(Player player, string hint)
    {
        _stringBuilder.AppendLine("<size=700%>\n</size>");
        _stringBuilder.AppendLine(FillMissingLines(hint));
        _stringBuilder.AppendLine("<align=right><size=45%><color=" + player.RoleBase.RoleColor.ToHex() + '>' + EntryPoint.Instance.SpectatorListConfig.SpectatorListTitle);

        int count = 0;
        foreach (Player spectator in Player.GetPlayers())
        {
            if (EntryPoint.Instance.SpectatorListConfig.SpectatorNames.Contains("(NONE)"))
                break;

            if (spectator.RoleBase is not SpectatorRole spectatorRole || spectatorRole.SyncedSpectatedNetId != player.NetworkId)
                continue;

            if (!EntryPoint.ShouldShowPlayer(spectator))
                continue;

            _stringBuilder.AppendLine(EntryPoint.Instance.SpectatorListConfig.SpectatorNames.Replace("(NAME)", spectator.Nickname));
            count++;
        }

        for (int i = count; i < 30; i++)
        {
            _stringBuilder.AppendLine();
        }

        _stringBuilder.Append("</color></size></align>");
        string ret = _stringBuilder.ToString().Replace("(COUNT)", count.ToString());
        _stringBuilder.Clear();
        return ret;
    }

    private static string FillMissingLines(string text, int linesNeeded = 6)
    {
        int textLines = text.Count(x => x == '\n');

        for (int i = 0; i < linesNeeded - textLines; i++)
            text += '\n';

        return text;
    }
}
