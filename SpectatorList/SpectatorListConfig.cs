using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpectatorList;

public sealed class SpectatorListConfig : IConfig
{
    [Description("Whether or not the plugin is enabled on this server")]
    public bool IsEnabled { get; set; } = true;

    [Description("Whether or not debug mode is enabled.")]
    public bool Debug { get; set; } = false;

    [Description("Whether or not people with Overwatch enabled should be ignored")]
    public bool IgnoreOverwatch { get; set; } = true;

    [Description("Whether or not Northwood staff should be ignored (Global Moderators will ALWAYS be ignored.)")]
    public bool IgnoreNorthwood { get; set; } = false;

    [Description("List of server roles that should be ignored")]
    public HashSet<string> IgnoredRoles { get; set; } = new();

    [Description("Set the Spectator List Title. Use (COUNT) to get the number of spectators")]
    public string SpectatorListTitle { get; set; } = "<b>👥 Spectators ((COUNT)):</b>";

    [Description("How names should be displayed. Use (NAME) to get the player's name; type (NONE) if you don't want to show their names.")]
    public string SpectatorNames { get; set; } = "(NAME)";

    [Description("The refresh rate of the hint")]
    public float RefreshRate { get; set; } = 1;
}