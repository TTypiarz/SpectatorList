using Exiled.API.Features;
using Exiled.Events;
using HarmonyLib;
using SpectatorList.Components;
using SpectatorList.EventHandlers;
using System;

namespace SpectatorList;

public class EntryPoint : Plugin<Config>
{
    public static EntryPoint Instance { get; private set; }

    public Harmony Harmony = new("spectatorlist.taj.com");

    public SpectatorListConfig SpectatorListConfig;

    public override string Name => "SpectatorList";
    public override string Author => "TTypiarz and Jesus-QC";
    public override Version Version => new(2, 0, 0);
    public override Version RequiredExiledVersion => new(6, 0, 0);

    public override void OnEnabled()
    {
        Instance = this;

        Exiled.Events.Handlers.Player.Verified += PlayerEventsHandler.OnPlayerJoined;

        SpectatorListController.RefreshRate = SpectatorListConfig.RefreshRate;

        Harmony.PatchAll();
    }

    public override void OnDisabled()
    {
        Harmony.UnpatchAll(Harmony.Id);

        Exiled.Events.Handlers.Player.Verified -= PlayerEventsHandler.OnPlayerJoined;

        Instance = null;
    }
}