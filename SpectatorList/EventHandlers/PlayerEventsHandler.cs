using System.Collections.Generic;
using Hints;
using InventorySystem.Items;
using PluginAPI.Core;
using PluginAPI.Enums;
using SpectatorList.Components;
using PluginAPI.Core.Attributes;

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

        if(!SpectatorListController.Controllers.ContainsKey(player) || hint is not TextHint textHint)
            return;

        SpectatorListController controller =  SpectatorListController.Controllers[Player.Get(display.netIdentity)];
        controller.savedHint = textHint.Text;
        controller.savedHintCounter = textHint.DurationScalar;
    }
}