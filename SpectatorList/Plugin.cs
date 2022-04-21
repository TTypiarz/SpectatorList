using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;

namespace SpectatorList
{
    public class Plugin : Plugin<Config, Translation>
    {
        public static Plugin Singleton;
        public EventHandlers EventHandlers;

        public override string Author { get; } = "TTypiarz";

        public override string Name { get; } = "SpectatorList";

        public override string Prefix { get; } = "SpectatorList";

        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        public override Version Version { get; } = new Version(1, 0, 0);

        public override void OnEnabled()
        {
            Singleton = this;
            EventHandlers = new EventHandlers(this);

            Player.Verified += EventHandlers.OnVerified;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Singleton = null;
            EventHandlers = null;

            Player.Verified -= EventHandlers.OnVerified;

            base.OnDisabled();
        }
    }
}