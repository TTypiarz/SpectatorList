using System.Collections.Generic;
using System.ComponentModel;

namespace SpectatorList;

public sealed class SpectatorListConfig
{
    [Description("Whether or not the plugin is enabled on this server.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Whether or not Debug mode is enabled.")]
    public bool Debug { get; set; } = false;

    [Description("Whether or not people with Overwatch enable should be Ignored.")]
    public bool IgnoreOverwatch { get; set; } = true;

    [Description("Whether or not Northwood Staff should be Ignored. (Global Moderators will ALWAYS be Ignored)")]
    public bool IgnoreNorthwood { get; set; } = false;

    [Description("List of Server Roles that should be Ignored.")]
    public HashSet<string> IgnoredRoles { get; set; } = new();

    [Description("Set the Spectator List Title - Use (COUNT) to get number of Spectators.")]
    public string SpectatorListTitle { get; set; } = "<b>👥 Spectators ((COUNT)):</b>";

    [Description("How names should be displayed - Use (NAME) to get player name, Type (NONE) if you don't want to show their names.")]
    public string SpectatorNames { get; set; } = "(NAME)";

    [Description("The refresh rate of the hint")]
    public float RefreshRate { get; set; } = 1;
}