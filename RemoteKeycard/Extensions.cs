using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Items.Keycards;
using Qurre.API;
using Qurre.API.Objects;
using System.Collections.Generic;
using System.Linq;

namespace RemoteKeycard
{
    public static class Extensions
    {
        public static bool CheckDoorPermissions(this Player player, DoorPermissions doorPermissions)
        {
            if (Equals(doorPermissions?.RequiredPermissions, KeycardPermissions.None) ||
                player.IsNullOrSCP())
            {
                return false;
            }

            foreach (KeycardItem keycardItem in player.GetKeycardsBase())
            {
                if (doorPermissions.CheckPermissions(keycardItem, player.ReferenceHub))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckKeycardPermissions(this Player player, KeycardPermissions keycardPermissions)
        {
            if (player.IsNullOrSCP())
                return false;

            foreach (KeycardItem keycardItem in player.GetKeycardsBase())
            {
                if (keycardItem?.Permissions.HasFlagFast(keycardPermissions) ?? false)
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool IsNotUnlocked(this GeneratorStatus generatorStatus)
        {
            return !Equals(generatorStatus, GeneratorStatus.Unlock);
        }

        private static bool IsNullOrSCP(this Player player)
        {
            return player?.RoleInfomation?.IsScp ?? true;
        }

        private static IEnumerable<KeycardItem> GetKeycardsBase(this Player player)
        {
            InventoryInfo inventoryInfo = player?.Inventory?.Base?.UserInventory;

            if (inventoryInfo?.Items == null)
                return null;

            return inventoryInfo.Items?.Values
                .Select(item => item as KeycardItem)
                .Where(item => item != null);
        }
    }
}
