using HarmonyLib;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using SpectatorList.Components;
using SpectatorList.EventHandlers;

namespace SpectatorList;

public class EntryPoint
{
    public static EntryPoint Instance { get; private set; }

    public Harmony Harmony = new("spectatorlist.taj.com");

    [PluginConfig]
    public SpectatorListConfig SpectatorListConfig;

    [PluginEntryPoint("SpectatorList", "2.0.0", "This Plugin allows you to see who is currently Spectating you and watching your every move.", "TTypiarz and Jesus-QC")]
    public void OnLoad()
    {
        Instance = this;
        EventManager.RegisterEvents<PlayerEventsHandler>(this);

        SpectatorListController.RefreshRate = SpectatorListConfig.RefreshRate;

        Harmony.PatchAll();
    }
}