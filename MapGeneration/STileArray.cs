using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;

namespace MinesRandomizer.MapGeneration
{
    public class STileArray
    {
        public Size mapSize;
        // linear representation of grid
        public STile[] sTiles;
        // pre-computing grid neighbors
        public List<Dictionary<GridDirection, int>> neighbors;

        // get from 2d to 1d coord
        private int sub2ind(int x, int y)
        {
            return y * mapSize.Width + x;
        }

        public STileArray(Size dimension)
        {
            mapSize = dimension;
            sTiles = new STile[dimension.Width * dimension.Height];
            neighbors = new List<Dictionary<GridDirection, int>>();
            for (int i = 0; i < mapSize.Width; i++)
            {
                for (int j = 0; j < mapSize.Height; j++)
                {
                    this[i, j] = new STile();
                    neighbors.Add(generateNeighbors(i, j));
                }
            }
        }

        private Dictionary<GridDirection, int> generateNeighbors(int x, int y)
        {
            Dictionary<GridDirection, int> tiles = new Dictionary<GridDirection, int>();
            if (x < 0 || x >= mapSize.Width || y < 0 || y >= mapSize.Height)
                return tiles;

            if (1 <= x) tiles.Add(GridDirection.LEFT, sub2ind(x - 1, y));
            if (1 <= y) tiles.Add(GridDirection.UP, sub2ind(x, y - 1));
            if (x + 1 < mapSize.Width) tiles.Add(GridDirection.RIGHT, sub2ind(x + 1, y));
            if (y + 1 < mapSize.Height) tiles.Add(GridDirection.DOWN, sub2ind(x, y + 1));

            return tiles;
        }

        public STile this[Location location]
        {
            get
            {
                return this.sTiles[mapSize.Width * location.Y + location.X];
            }
            set
            {
                this.sTiles[mapSize.Width * location.Y + location.X] = value;
            }
        }
        public STile this[int index]
        {
            get
            {
                return this.sTiles[index];
            }
            set
            {
                this.sTiles[index] = value;
            }
        }
        public STile this[int x, int y]
        {
            get
            {
                return this.sTiles[mapSize.Width * y + x];
            }
            set
            {
                this.sTiles[mapSize.Width * y + x] = value;
            }
        }

        public IEnumerator<Tuple<GridDirection, STile>> TileNeighbors(int index)
        {
            foreach (var neigh in neighbors[index])
            {
                yield return new Tuple<GridDirection, STile>(neigh.Key, this[neigh.Value]);
            }
        }

        public IEnumerator<Tuple<GridDirection, STile>> TileNeighbors(int x, int y)
        {
            foreach (var neigh in neighbors[sub2ind(x, y)])
            {
                yield return new Tuple<GridDirection, STile>(neigh.Key, this[neigh.Value]);
            }
        }
    }
}
