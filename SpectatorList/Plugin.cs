using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace SpectatorList;

public class Plugin
{
    public static Plugin Instance { get; private set; }

    [PluginConfig] public Config Config;

    [PluginEntryPoint("SpectatorList", "1.1.4", "This Plugin allows you to see who is currently Spectating you and watching your every move.", "TTypiarz")]
    public void OnLoad()
    {
        Instance = this;
        EventManager.RegisterEvents<EventHandlers>(this);
    }
}