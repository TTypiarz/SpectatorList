using Exiled.Events.EventArgs.Player;
using Hints;
using SpectatorList.Components;
using Hint = Hints.Hint;
using Player = Exiled.API.Features.Player;

namespace SpectatorList.EventHandlers;

// With love by Jesus-QC <3
public class PlayerEventsHandler
{
    public static void OnPlayerJoined(VerifiedEventArgs ev)
    {
        ev.Player.GameObject.AddComponent<SpectatorListController>().Init(ev.Player);
    }

    public static void ShowHint(HintDisplay display, Hint hint)
    {
        Player player = Player.Get(display.netIdentity);

        if (!SpectatorListController.Controllers.ContainsKey(player) || hint is not TextHint textHint)
            return;

        SpectatorListController controller = SpectatorListController.Controllers[Player.Get(display.netIdentity)];
        controller.savedHint = textHint.Text;
        controller.savedHintCounter = textHint.DurationScalar;
    }
}