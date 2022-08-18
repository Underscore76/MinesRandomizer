# MinesRandomizer

Currently just a PoC of "Can you load new Layers into a xTile.Map at map generation time"

## Map Gen

Each Map is made of 3 Layers (Back, Building, Front) made out of StaticTiles. Basically just need to load a dummy map, nuke the existing layers and push in new ones with updated tile data.

## Harmony Patches

* MineShaft.LoadLevel
  * Prefix skip the normal code
  * Rerun the code to clear the mineshaft state
  * Load floor 1
  * "Generate a map"
  * Write the new map layers
* MineShaft.prepareElevator + MineShaft.setElevatorLift
  * Block the elevator from getting generatd on different floors
