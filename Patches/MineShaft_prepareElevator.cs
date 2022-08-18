using StardewModdingAPI;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesRandomizer.Patches
{
    class MineShaft_prepareElevator
    {
        private static IMonitor Monitor;
        private static IReflectionHelper Reflection;
        public static void Initialize(IMonitor monitor, IReflectionHelper reflection)
        {
            Monitor = monitor;
            Reflection = reflection;
        }

        // we don't want to create an elevator for now...
        public static bool Prefix(MineShaft __instance)
        {
            return false;
        }

        public static void Postfix(MineShaft __instance)
        {

        }
    }
}
