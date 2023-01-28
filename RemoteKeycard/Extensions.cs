using Qurre.API;
using Qurre.API.Controllers;
using System.Collections.Generic;

namespace RemoteKeycard
{
    internal static class Extensions
    {
        public static IReadOnlyCollection<Item> GetItems(this Player player)
        {
            return player?.Inventory?.Items?.Values;
        }
    }
}
