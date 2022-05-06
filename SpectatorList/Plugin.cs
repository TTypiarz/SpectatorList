using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;

namespace SpectatorList
{
    public class Plugin : Plugin<Config, Translation>
    {
        public override string Author => "TTypiarz";

        public override string Name => "SpectatorList";

        public override string Prefix => "SpectatorList";

        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        public override Version Version { get; } = new Version(1, 1, 1);
        
        public static Plugin Singleton { get; private set; }
        
        private EventHandlers _eventHandlers;

        private Plugin() {}
        
        public override void OnEnabled()
        {
            Singleton = this;
            
            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            
            Singleton = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _eventHandlers = new EventHandlers();

            Player.Verified += _eventHandlers.OnVerified;
        }

        private void UnregisterEvents()
        {
            Player.Verified -= _eventHandlers.OnVerified;

            _eventHandlers = null;
        }
    }
}