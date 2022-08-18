using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Locations;
using StardewValley;
using StardewModdingAPI;
using StardewValley.Menus;
using xTile;
using xTile.Layers;
using xTile.Dimensions;
using MinesRandomizer.MapGeneration;
using Microsoft.Xna.Framework;

namespace MinesRandomizer.Patches
{
    class MineShaft_loadLevel
    {
        private static IMonitor Monitor;
        private static IReflectionHelper Reflection;

        // call this method from your Entry class
        public static void Initialize(IMonitor monitor, IReflectionHelper reflection)
        {
            Monitor = monitor;
            Reflection = reflection;
        }
        public static bool Prefix(int level, MineShaft __instance)
        {
            return false;
        }

        public static void ResetMinesState(MineShaft mineShaft)
        {
            string[] propFields = { "isMonsterArea", "isSlimeArea", "isQuarryArea", "isDinoArea" };
            string[] boolFields = { "loadedDarkArea" };
            foreach (string fieldName in propFields)
            {
                IReflectedProperty<bool> field = Reflection.GetProperty<bool>(mineShaft, fieldName);
                field.SetValue(false);
            }
            foreach (string fieldName in boolFields)
            {
                IReflectedField<bool> field = Reflection.GetField<bool>(mineShaft, fieldName);
                field.SetValue(false);
            }
        }

        public static void ResetLoader(MineShaft mineShaft)
        {
            IReflectedField<LocalizedContentManager> field = Reflection.GetField<LocalizedContentManager>(mineShaft, "mineLoader");
            LocalizedContentManager mineLoader = field.GetValue();
            mineLoader.Unload();
            mineLoader.Dispose();
            field.SetValue(Game1.content.CreateTemporary());
        }

        public static void Postfix(int level, ref MineShaft __instance)
        {
            Monitor.Log("ran default floor gen");
            ResetMinesState(__instance);
            ResetLoader(__instance);

            // this loads the a clone of map that will be overwritten
            int mapNumberToLoad = 1;
            string tileSheetSource = "mine";
            __instance.mapPath.Value = "Maps\\Mines\\" + mapNumberToLoad;
            __instance.loadedMapNumber = mapNumberToLoad;
            __instance.updateMap();
            __instance.mapImageSource.Value = tileSheetSource;

            // generate and set up the actual layers
            SMap map = MapGenerator.Generate(new Size(12, 15), tileSheetSource);
            map.OverwriteMapLayers(__instance.map);

            // ensure tiles have props
            __instance.ApplyDiggableTileFixes();
        }
    }
}
