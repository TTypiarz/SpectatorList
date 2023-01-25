using Exiled.API.Features;
using System;

namespace SpectatorList;

public class Plugin : Plugin<Config, Translation>
{
    private EventHandlers EventHandlers;

    public override string Author { get; } = "TTypiarz";

    public override string Name { get; } = "SpectatorList";

    public override string Prefix { get; } = "SpectatorList";

    public override Version RequiredExiledVersion { get; } = new Version(6, 0, 0);

    public override Version Version { get; } = new Version(1, 1, 6);

    public override void OnEnabled()
    {
        EventHandlers = new EventHandlers(this);
        Exiled.Events.Handlers.Player.Spawning += EventHandlers.OnSpawning;

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Spawning -= EventHandlers.OnSpawning;
        EventHandlers = null;

        base.OnDisabled();
    }
}