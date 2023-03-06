using Exiled.API.Features;
using HarmonyLib;
using SpectatorList.Components;
using SpectatorList.EventHandlers;
using System;
using Player = Exiled.Events.Handlers.Player;

namespace SpectatorList;

public class EntryPoint : Plugin<SpectatorListConfig>
{
    public static EntryPoint Instance { get; private set; }

    public Harmony Harmony = new("spectatorlist.taj.com");

    public override string Name => "SpectatorList";
    public override string Author => "TTypiarz and Jesus-QC";
    public override Version Version => new(2, 0, 0);
    public override Version RequiredExiledVersion => new(6, 0, 0);

    public override void OnEnabled()
    {
        Instance = this;

        Player.Verified += PlayerEventsHandler.OnPlayerJoined;

        SpectatorListController.RefreshRate = Instance.Config.RefreshRate;

        Harmony.PatchAll();
    }

    public override void OnDisabled()
    {
        Harmony.UnpatchAll(Harmony.Id);

        Player.Verified -= PlayerEventsHandler.OnPlayerJoined;

        Instance = null;
    }
}