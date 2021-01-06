#pragma warning disable CS0618
using Qurre.API;
using Qurre.API.Events;
using System.Linq;
namespace remote_keycard
{
	public class Plugin : Qurre.Plugin
	{
		#region override
		public override string name => "remote keycard";
		public override void Enable() => RegisterEvents();
		public override void Disable() => UnregisterEvents();
		public override void Reload() { }
		#endregion
		#region events
		private void RegisterEvents()
		{
			Qurre.Events.Player.InteractDoor += RunOnDoorOpen;
			Qurre.Events.Player.InteractLocker += LockerInteraction;
			Qurre.Events.Player.InteractGenerator += GenOpen;
		}
		private void UnregisterEvents()
		{
			Qurre.Events.Player.InteractDoor -= RunOnDoorOpen;
			Qurre.Events.Player.InteractLocker -= LockerInteraction;
			Qurre.Events.Player.InteractGenerator -= GenOpen;
		}
		#endregion
		#region main
		public void RunOnDoorOpen(InteractDoorEvent ev)
		{
			if (ev.Player.GetTeam() == Team.SCP) return;
			if (!ev.IsAllowed)
			{
				var playerIntentory = ev.Player.inventory.items;
				foreach (var item in playerIntentory)
				{
					var gameItem = UnityEngine.Object.FindObjectOfType<Inventory>().availableItems.FirstOrDefault(i => i.id == item.id);
					if (gameItem == null)
						continue;
					if (ev.Door.RequiredPermissions.CheckPermissions(gameItem, ev.Player))
					{
						ev.IsAllowed = true;
					}
				}
			}
		}
		public void LockerInteraction(InteractLockerEvent ev)
		{
			if (!ev.IsAllowed)
			{
				if (ev.Player.GetTeam() == Team.SCP) return;
				var playerIntentory = ev.Player.inventory.items;
				bool chcb = false;
				bool lvl2per = false;
				foreach (var item in playerIntentory)
				{
					var gameItem = UnityEngine.Object.FindObjectOfType<Inventory>().availableItems.FirstOrDefault(i => i.id == item.id);
					if (gameItem == null)
						continue;
					if (gameItem.permissions == null || gameItem.permissions.Length == 0)
						continue;
					foreach (var itemPerm in gameItem.permissions)
					{
						if (itemPerm == "PEDESTAL_ACC")
						{
							ev.IsAllowed = true;
							continue;
						}
						if (itemPerm == "CHCKPOINT_ACC")
						{
							chcb = true;
						}
						if (itemPerm == "CONT_LVL_2")
						{
							lvl2per = true;
						}
					}
					if (chcb && lvl2per)
					{
						ev.IsAllowed = true;
						continue;
					}
				}
			}
		}
		public void GenOpen(InteractGeneratorEvent ev)
		{
			if (ev.Player.GetTeam() == Team.SCP) return;
			var playerIntentory = ev.Player.inventory.items;
			foreach (var item in playerIntentory)
			{
				var gameItem = UnityEngine.Object.FindObjectOfType<Inventory>().availableItems.FirstOrDefault(i => i.id == item.id);
				if (gameItem == null)
					continue;
				if (gameItem.permissions == null || gameItem.permissions.Length == 0)
					continue;
				foreach (var itemPerm in gameItem.permissions)
				{
					if (itemPerm == "ARMORY_LVL_2")
					{
						ev.IsAllowed = true;
						continue;
					}
				}
			}
		}
		#endregion
	}
}