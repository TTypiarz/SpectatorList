using HarmonyLib;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using SpectatorList.Components;
using SpectatorList.EventHandlers;
using System;

namespace SpectatorList;

public class EntryPoint
{
    public static EntryPoint Instance { get; private set; }

    public Harmony Harmony = new($"ttypiarz-spectatorlist-{DateTime.UtcNow.Ticks}");

    [PluginConfig]
    public SpectatorListConfig SpectatorListConfig;

    [PluginEntryPoint("SpectatorList", "2.1.0", "This plugin allows you to see who is currently spectating you and watching your every move", "TTypiarz & Jesus-QC")]
    public void OnLoad()
    {
        Instance = this;
        EventManager.RegisterEvents<PlayerEventsHandler>(this);
        SpectatorListController.RefreshRate = SpectatorListConfig.RefreshRate;
        Harmony.PatchAll();
    }

    public static bool ShouldShowPlayer(Player player)
    {
        if (player.IsGlobalModerator || player.IsOverwatchEnabled && Instance.SpectatorListConfig.IgnoreOverwatch || player.IsNorthwoodStaff && Instance.SpectatorListConfig.IgnoreNorthwood)
            return false;

        if (Instance.SpectatorListConfig.IgnoredRoles.Contains(player.ReferenceHub.serverRoles.GetUncoloredRoleString()))
            return false;

        return true;
    }
}