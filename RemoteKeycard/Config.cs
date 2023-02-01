using Qurre.API.Addons;

namespace RemoteKeycard
{
    public static class Config
    {
        public static JsonConfig JConfig { get; private set; }

        public static bool EnableForDoors { get; private set; }
        public static bool EnableForLockers { get; private set; }
        public static bool EnableForGenerators { get; private set; }

        public static void Setup()
        {
            JConfig = new JsonConfig("RemoteKeycard");

            EnableForDoors      = JConfig.SafeGetValue("EnableForDoors",      true, "Enable RemoteKeycard for Doors?");
            EnableForLockers    = JConfig.SafeGetValue("EnableForLockers",    true, "Enable RemoteKeycard for Lockers?");
            EnableForGenerators = JConfig.SafeGetValue("EnableForGenerators", true, "Enable RemoteKeycard for Generators?");

            JsonConfig.UpdateFile();
        }
    }
}
