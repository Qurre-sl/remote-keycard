using Interactables.Interobjects.DoorUtils;
using PlayerRoles;
using Qurre.API;
using Qurre.API.Addons.Items;
using Qurre.API.Attributes;
using Qurre.API.Classification.Player;
using Qurre.API.Controllers;
using Qurre.API.Objects;
using Qurre.Events;
using Qurre.Events.Structs;

namespace RemoteKeycard
{
    [PluginInit("RemoteKeycard", "Qurre Team", "2.0.0")]
    internal static class Plugin
    {
        [EventMethod(PlayerEvents.InteractDoor)]
        public static void OnInteractDoorEvent(IBaseEvent eventBase)
        {
            if (eventBase is InteractDoorEvent interactDoorEvent && interactDoorEvent.Allowed == false)
            {
                DoorPermissions doorPermissions = interactDoorEvent.Door?.Permissions;
                RoleInfomation roleInformation = interactDoorEvent.Player?.RoleInfomation;

                if (Equals(doorPermissions?.RequiredPermissions, KeycardPermissions.None) ||
                Equals(roleInformation?.Team, Team.SCPs))
                {
                    return;
                }

                foreach (Item item in interactDoorEvent.Player?.GetItems())
                {
                    if (item == null)
                        continue;

                    if (doorPermissions.CheckPermissions(item.Base, interactDoorEvent.Player?.ReferenceHub))
                    {
                        interactDoorEvent.Allowed = true;
                        return;
                    }
                }
            }
        }

        [EventMethod(PlayerEvents.InteractLocker)]
        public static void OnInteractLockerEvent(IBaseEvent eventBase)
        {
            if (eventBase is InteractLockerEvent interactLockerEvent && interactLockerEvent.Allowed == false)
            {
                Player player = interactLockerEvent.Player;

                if (Equals(player?.RoleInfomation?.Team, Team.SCPs))
                {
                    return;
                }

                foreach (Item item in player?.GetItems())
                {
                    if (item is Keycard keycard &&
                    keycard.Permissions.HasFlagFast(KeycardPermissions.ContainmentLevelTwo) &&
                    keycard.Permissions.HasFlagFast(KeycardPermissions.Checkpoints))
                    {
                        interactLockerEvent.Allowed = true;
                        return;
                    }
                }
            }
        }

        [EventMethod(PlayerEvents.InteractGenerator)]
        public static void OnInteractGeneratorEvent(IBaseEvent eventBase)
        {
            if (eventBase is InteractGeneratorEvent interactGeneratorEvent && !Equals(interactGeneratorEvent.Status, GeneratorStatus.Unlock))
            {
                Player player = interactGeneratorEvent?.Player;

                if (Equals(player?.RoleInfomation?.Team, Team.SCPs))
                {
                    return;
                }

                foreach (Item item in player?.GetItems())
                {
                    if (item is Keycard keycard &&
                    keycard.Permissions.HasFlagFast(KeycardPermissions.ArmoryLevelTwo))
                    {
                        interactGeneratorEvent.Allowed = true;
                        return;
                    }
                }
            }
        }
    }
}
