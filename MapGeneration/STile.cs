using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesRandomizer.MapGeneration
{
    public class STile
    {
        public HashSet<int> Back;
        public HashSet<int> Buildings;
        public HashSet<int> Front;

        public STile() : this(-1, -1, -1)
        {
        }

        public STile(int back, int buildings, int front)
        {
            Back = new();
            Buildings = new();
            Front = new();
            if (back != -1) Back.Add(back);
            if (buildings != -1) Buildings.Add(buildings);
            if (front != -1) Front.Add(front);
        }

        public STile Copy()
        {
            STile n = new();
            n.Back.UnionWith(this.Back);
            n.Buildings.UnionWith(this.Buildings);
            n.Front.UnionWith(this.Front);
            return n;
        }
    }
}
