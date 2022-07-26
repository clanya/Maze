﻿using System;
using Maze;

namespace Game.Maze
{
    [Serializable]
    public struct MazeLevel
    {
        public MazeGeneratorType mazeGeneratorType;
        public int width;
        public int height;

        public MazeLevel(MazeGeneratorType type, int width, int height)
        {
            mazeGeneratorType = type;
            this.width = width;
            this.height = height;
        }
    }
}