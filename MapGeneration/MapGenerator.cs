using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;

namespace MinesRandomizer.MapGeneration
{
    class MapGenerator
    {

        public static SMap Generate(Size dimension, string mapTileSheet)
        {
            SMap map = new SMap(new Size(24, 24), mapTileSheet);

            map.SetBoundaryTile(GridDirection.LEFT, new STile(-1, 77, -1));
            map.SetBoundaryTile(GridDirection.RIGHT, new STile(-1, 77, -1));
            map.SetBoundaryTile(GridDirection.UP, new STile(-1, 77, -1));
            map.SetBoundaryTile(GridDirection.DOWN, new STile(-1, 77, -1));
            map.SetBackground(185);
            map.SetLadderSpot(1, 1);

            return map;
        }
    }
}
