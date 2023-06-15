using Exiled.API.Features;
using Hints;
using PlayerRoles.Spectating;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SpectatorList.Components;

// With love by Jesus-QC <3
public class SpectatorListController : MonoBehaviour
{
    public static float RefreshRate = 1;

    public static readonly Dictionary<Player, SpectatorListController> Controllers = new();

    private Player _player;
    private float _counter;

    private CustomHintDisplay _display;
    public string savedHint = string.Empty;
    public float savedHintCounter;

    public void Init(Player player) => _player = player;

    private void OnDestroy()
    {
        Controllers.Remove(_player);
        _display.Clear();
        _display = null;
    }

    private void Start()
    {
        _display = new CustomHintDisplay();
        Controllers.Add(_player, this);
    }

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter < RefreshRate)
            return;

        UpdateHintCounter();

        _counter = 0;
        DrawHud();
    }

    private void UpdateHintCounter()
    {
        if (savedHintCounter < 0)
            return;

        savedHintCounter -= 0.5f;

        if (savedHintCounter >= 0)
            return;

        savedHint = string.Empty;
    }

    private async void DrawHud()
    {
        if (!Round.IsStarted || !_player.IsAlive)
            return;

        int spectators = 0;
        foreach (Player player in Player.List)
        {
            if (!_player.ReferenceHub.IsSpectatedBy(player.ReferenceHub))
                continue;

            if (player.Role.Base is not SpectatorRole spectatorRole || spectatorRole.SyncedSpectatedNetId != _player.NetId)
                continue;

            if (!EntryPoint.ShouldShowPlayer(player))
                continue;

            spectators++;
        }

        if (spectators <= 0)
            return;

        string hint = await Task.Run(() => _display.Draw(_player, savedHint));
        _player.Connection.Send(new HintMessage(new TextHint(hint, new[] { new StringHintParameter(string.Empty) })));
    }
}