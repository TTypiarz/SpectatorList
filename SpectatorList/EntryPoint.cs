using Exiled.API.Features;
using HarmonyLib;
using SpectatorList.Components;
using SpectatorList.EventHandlers;
using System;

namespace SpectatorList;

public class EntryPoint : Plugin<SpectatorListConfig>
{
    public static EntryPoint Instance { get; private set; }

    public Harmony Harmony = new($"ttypiarz-spectatorlist-{DateTime.UtcNow.Ticks}");

    public override string Name => "SpectatorList";
    public override string Author => "TTypiarz & Jesus-QC";
    public override Version Version => new(2, 1, 0);
    public override Version RequiredExiledVersion => new(7, 0, 0);

    public override void OnEnabled()
    {
        Instance = this;

        Exiled.Events.Handlers.Player.Verified += PlayerEventsHandler.OnPlayerJoined;
        SpectatorListController.RefreshRate = Instance.Config.RefreshRate;
        Harmony.PatchAll();
    }

    public override void OnDisabled()
    {
        Harmony.UnpatchAll(Harmony.Id);
        Exiled.Events.Handlers.Player.Verified -= PlayerEventsHandler.OnPlayerJoined;
        Instance = null;
    }

    public static bool ShouldShowPlayer(Player player)
    {
        if (player.IsGlobalModerator || player.IsOverwatchEnabled && Instance.Config.IgnoreOverwatch || player.IsNorthwoodStaff && Instance.Config.IgnoreNorthwood)
            return false;

        if (Instance.Config.IgnoredRoles.Contains(player.ReferenceHub.serverRoles.GetUncoloredRoleString()))
            return false;

        return true;
    }
}