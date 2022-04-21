using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SpectatorList
{
    public sealed class Translation : ITranslation
    {
        [Description("Set the Spectator list Title - Use (COUNT) to get number of Spectators, Use (COLOR) to get current Role color.")]
        public string Title { get; set; } = "<align=right><size=45%><color=(COLOR)><b>👥 Spectators ((COUNT)):</b></color></size></align>";

        [Description("How names should be displayed - Use (NAME) to get player name, Type (none) if you don't want to show their names.")]
        public string Names { get; set; } = "<align=right><size=45%><color=(COLOR)>(NAME)</color></size></align>";
    }
}