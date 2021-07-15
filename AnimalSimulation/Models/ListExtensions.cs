using System.Collections.Generic;
using System.Linq;

namespace AnimalSimulation.Models
{
    public static class ListExtensions
    {
        public static Cell At(this List<Cell> cells, int x, int y)
        {
            return cells.FirstOrDefault(c => c.Position.X == x && c.Position.Y == y);
        }
    }
}
