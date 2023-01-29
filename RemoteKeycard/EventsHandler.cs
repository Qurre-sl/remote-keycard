using Interactables.Interobjects.DoorUtils;
using Qurre.API.Attributes;
using Qurre.Events.Structs;
using Qurre.Events;

namespace RemoteKeycard
{
    internal static class EventsHandler
    {
        [EventMethod(PlayerEvents.InteractDoor)]
        public static void OnInteractDoorEvent(IBaseEvent eventBase)
        {
            if (Config.EnableForDoors &&
                eventBase is InteractDoorEvent interactDoorEvent &&
                interactDoorEvent.Allowed == false &&
                interactDoorEvent.Player.CheckDoorPermissions(interactDoorEvent.Door?.Permissions))
            {
                interactDoorEvent.Allowed = true;
            }
        }

        [EventMethod(PlayerEvents.InteractLocker)]
        public static void OnInteractLockerEvent(IBaseEvent eventBase)
        {
            if (Config.EnableForLockers &&
                eventBase is InteractLockerEvent interactLockerEvent &&
                interactLockerEvent.Allowed == false &&
                interactLockerEvent.Player.CheckKeycardPermissions(KeycardPermissions.ContainmentLevelTwo | KeycardPermissions.Checkpoints))
            {
                interactLockerEvent.Allowed = true;
            }
        }

        [EventMethod(PlayerEvents.InteractGenerator)]
        public static void OnInteractGeneratorEvent(IBaseEvent eventBase)
        {
            if (Config.EnableForGenerators &&
                eventBase is InteractGeneratorEvent interactGeneratorEvent &&
                interactGeneratorEvent.Allowed == false &&
                interactGeneratorEvent.Status.IsNotUnlocked() &&
                interactGeneratorEvent.Player.CheckKeycardPermissions(KeycardPermissions.ArmoryLevelTwo))
            {
                interactGeneratorEvent.Allowed = true;
            }
        }
    }
}
