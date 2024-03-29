﻿using Hints;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using SpectatorList.Components;

namespace SpectatorList.EventHandlers;

// With love by Jesus-QC <3
public class PlayerEventsHandler
{
    [PluginEvent(ServerEventType.PlayerJoined)]
    public void OnPlayerJoined(Player player)
    {
        player.GameObject.AddComponent<SpectatorListController>().Init(player);
    }

    public static void ShowHint(HintDisplay display, Hint hint)
    {
        Player player = Player.Get(display.netIdentity);

        if (!player.IsAlive)
            return;

        if (!SpectatorListController.Controllers.ContainsKey(player) || hint is not TextHint textHint)
            return;

        SpectatorListController controller = SpectatorListController.Controllers[player];
        controller.savedHint = textHint.Text;
        controller.savedHintCounter = textHint.DurationScalar;
    }
}
