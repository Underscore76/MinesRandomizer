using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Locations;
using HarmonyLib;
using System.Reflection;

namespace MinesRandomizer
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            var harmony = new Harmony(this.ModManifest.UniqueID);

            Patches.MineShaft_loadLevel.Initialize(this.Monitor, helper.Reflection);

            harmony.Patch(
                original: AccessTools.Method(typeof(MineShaft), nameof(MineShaft.loadLevel)),
                prefix: new HarmonyMethod(typeof(Patches.MineShaft_loadLevel), nameof(Patches.MineShaft_loadLevel.Prefix)),
                postfix: new HarmonyMethod(typeof(Patches.MineShaft_loadLevel), nameof(Patches.MineShaft_loadLevel.Postfix))
                );

            Patches.MineShaft_prepareElevator.Initialize(this.Monitor, helper.Reflection);
            harmony.Patch(
                original: AccessTools.Method(typeof(MineShaft), "prepareElevator"),
                prefix: new HarmonyMethod(typeof(Patches.MineShaft_prepareElevator), nameof(Patches.MineShaft_prepareElevator.Prefix)),
                postfix: new HarmonyMethod(typeof(Patches.MineShaft_prepareElevator), nameof(Patches.MineShaft_prepareElevator.Postfix))
                );

            Patches.MineShaft_setElevatorLift.Initialize(this.Monitor, helper.Reflection);
            harmony.Patch(
                original: AccessTools.Method(typeof(MineShaft), "setElevatorLit"),
                prefix: new HarmonyMethod(typeof(Patches.MineShaft_setElevatorLift), nameof(Patches.MineShaft_setElevatorLift.Prefix)),
                postfix: new HarmonyMethod(typeof(Patches.MineShaft_setElevatorLift), nameof(Patches.MineShaft_setElevatorLift.Postfix))
                );
        }
    }
}