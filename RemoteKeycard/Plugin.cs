using Qurre.API.Attributes;

namespace RemoteKeycard
{
    [PluginInit("RemoteKeycard", "Qurre Team", "2.1.0")]
    internal static class Plugin
    {
        [PluginEnable]
        public static void OnPluginEnable()
        {
            Config.Setup();
        }
    }
}
