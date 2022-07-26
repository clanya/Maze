using System.Collections.Generic;
using Maze;
using UnityEngine;

namespace Utility
{
    public class MazeGeneratorParameter
    {
        public List<MazeGeneratorType> mazeGeneratorTypes = new List<MazeGeneratorType>();
        public List<int> widths = new List<int>();
        public List<int> heights = new List<int>();
    }
}