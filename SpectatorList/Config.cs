using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpectatorList;

public sealed class Config : IConfig
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
    public List<string> IgnoredRoles { get; set; } = new List<string>();
}