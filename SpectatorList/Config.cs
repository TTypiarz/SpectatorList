using PluginAPI.Core.Interfaces;
using System.ComponentModel;

namespace SpectatorList
{
    public sealed class PluginConfig : IPluginConfig
    {
        [Description("Whether or not the plugin is enabled on this server.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not people with Overwatch enable should be Ignored.")]
        public bool IgnoreOverwatch { get; set; } = true;

        [Description("Whether or not Northwood Staff should be Ignored. (Global Moderators will ALWAYS be Ignored)")]
        public bool IgnoreNorthwood { get; set; } = false;

        [Description("Set the Spectator list Title - Use (COUNT) to get number of Spectators, Use (COLOR) to get current Role color.")]
        public string Title { get; set; } = "<align=right><size=45%><color=(COLOR)><b>👥 Spectators ((COUNT)):</b></color></size></align>";

        [Description("How names should be displayed - Use (NAME) to get player name, Type (none) if you don't want to show their names.")]
        public string Names { get; set; } = "<align=right><size=45%><color=(COLOR)>\n(NAME)</color></size></align>";

        public bool ShowDebugMessages { get; set; } = false;
    }
}