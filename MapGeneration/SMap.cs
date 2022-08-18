using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;
using xTile.Tiles;
using xTile.Layers;
using xTile;
using StardewValley.Locations;

namespace MinesRandomizer.MapGeneration
{
    public class SMap
    {
        public readonly Size tileSize = new Size(64, 64);
        public Size mapSize;
        public STileArray Tiles;
        public string tileSheetId;

        public SMap(Size dimensions, string tileSheetSource)
        {
            mapSize = dimensions;
            Tiles = new STileArray(dimensions);
            tileSheetId = tileSheetSource;
        }

        public void SetBoundaryTile(GridDirection dir, STile tile)
        {
            switch (dir)
            {
                case GridDirection.UP:
                    for (int i = 0; i < mapSize.Width; i++)
                    {
                        Tiles[i, 0] = tile.Copy();
                    }
                    break;
                case GridDirection.DOWN:
                    for (int i = 0; i < mapSize.Width; i++)
                    {
                        Tiles[i, mapSize.Height - 1] = tile.Copy();
                    }
                    break;
                case GridDirection.LEFT:
                    for (int i = 0; i < mapSize.Height; i++)
                    {
                        Tiles[0, i] = tile.Copy();
                    }
                    break;
                case GridDirection.RIGHT:
                    for (int i = 0; i < mapSize.Height; i++)
                    {
                        Tiles[mapSize.Width - 1, i] = tile.Copy();
                    }
                    break;
            }
        }

        public void SetBackground(int tileIndex)
        {
            foreach (var t in Tiles.sTiles)
            {
                t.Back.Add(tileIndex);
            }
        }

        public void OverwriteMapLayers(Map map)
        {
            // nuke the existing layers
            string[] layerNames = { "Back", "Buildings", "Front" };
            foreach (var layerName in layerNames)
            {
                if (map.GetLayer(layerName) != null)
                    map.RemoveLayer(map.GetLayer(layerName));

                map.AddLayer(new Layer(layerName, map, mapSize, tileSize));
            }

            Layer Back = map.GetLayer("Back");
            Layer Buildings = map.GetLayer("Buildings");
            Layer Front = map.GetLayer("Front");
            TileSheet tileSheet = map.GetTileSheet(tileSheetId);

            for (int i = 0; i < mapSize.Width; i++)
            {
                for (int j = 0; j < mapSize.Height; j++)
                {
                    STile tile = Tiles[i, j];
                    // NOTE: these arrays may have MORE than one, just pick thee first
                    if (tile.Back.Count > 0)
                        Back.Tiles[i, j] = new StaticTile(Back, tileSheet, BlendMode.Alpha, tile.Back.First());
                    if (tile.Buildings.Count > 0)
                        Buildings.Tiles[i, j] = new StaticTile(Buildings, tileSheet, BlendMode.Alpha, tile.Buildings.First());
                    if (tile.Front.Count > 0)
                        Front.Tiles[i, j] = new StaticTile(Front, tileSheet, BlendMode.Alpha, tile.Front.First());
                }
            }
        }

        public void SetLadderSpot(int x, int y)
        {
            Tiles[x, y].Buildings.Add(115);
        }
    }
}
