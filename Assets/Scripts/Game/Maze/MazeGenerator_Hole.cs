using System.Collections.Generic;
using System.Diagnostics;
using Maze;

namespace Game.Maze
{
    public class MazeGenerator_Hole : BaseMazeGenerator
    {
        private readonly int[,] maze;
        private readonly int width;
        private readonly int height;
        private List<(int x, int y)> candidateStartingCoordinates = new List<(int, int)>();

        public MazeGenerator_Hole(int[,] maze)
        {
            this.maze = maze;
            width = maze.GetLength(0);
            height = maze.GetLength(1);
            Debug.Assert(height % 2 == 1 && width % 2 == 1,"Set the argument to an odd number!!");
            Debug.Assert(width > MazeConfig.MinimumValue && height > MazeConfig.MinimumValue,"Set the argument to a number greater than minimum value(5)");
        }
        
        private MazeGenerator_Hole(){ }

        public override void GenerateMaze()
        {
            UnityEngine.Debug.Log($"Hole width:{width},height{height}");
            ReadyToGenerateMaze();
            GetCandidateStartingCoordinates();
            DecideValue();
            CleanUp();
        }
        
        /// <summary>
        /// 初期化.外周はPath.他はWall.
        /// </summary>
        private void ReadyToGenerateMaze()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        maze[x, y] = MazeConfig.Path;
                    }
                    else
                    {
                        maze[x, y] = MazeConfig.Wall;
                    }
                }
            }
        }
        
        /// <summary>
        /// 穴掘りの開始位置候補の座標をリストアップ.
        /// </summary>
        private void GetCandidateStartingCoordinates()
        {
            for (int y = 1; y < height; y += 2)
            {
                for (int x = 1; x < width; x += 2)
                {
                    candidateStartingCoordinates.Add((x,y));
                }
            }
        }
        
        private void DecideValue()
        {
            var startCoordinate = GetStartCoordinate();
            DigHole(startCoordinate.x,startCoordinate.y);
        }

        private void DigHole(int x, int y)
        {
            while (candidateStartingCoordinates.Count > 0)
            {
                var directions = new List<MazeConfig.Direction>();
                if (maze[x, y - 1] == MazeConfig.Wall && maze[x, y - 2] == MazeConfig.Wall)
                {
                    directions.Add(MazeConfig.Direction.Up);
                }
                if (maze[x + 1, y] == MazeConfig.Wall && maze[x + 2, y] == MazeConfig.Wall)
                {
                    directions.Add(MazeConfig.Direction.Right);
                }
                if (maze[x, y + 1] == MazeConfig.Wall && maze[x, y + 2] == MazeConfig.Wall)
                {
                    directions.Add(MazeConfig.Direction.Down);
                }
                if (maze[x - 1, y] == MazeConfig.Wall && maze[x - 2, y] == MazeConfig.Wall)
                {
                    directions.Add(MazeConfig.Direction.Left);
                }

                if (directions.Count == 0)
                {
                    break;
                }
            
                SetPath(x,y);

                var direction = directions[UnityEngine.Random.Range(0, directions.Count)];
                switch (direction)
                {
                    case MazeConfig.Direction.Up:
                        SetPath(x, --y);
                        SetPath(x, --y);
                        break;
                    case MazeConfig.Direction.Right:
                        SetPath(++x, y);
                        SetPath(++x, y);
                        break;
                    case MazeConfig.Direction.Down:
                        SetPath(x, ++y);
                        SetPath(x, ++y);
                        break;
                    case MazeConfig.Direction.Left:
                        SetPath(--x, y);
                        SetPath(--x, y);
                        break;
                }
                
            }

            if (candidateStartingCoordinates.Count > 0)
            {
                var startCoordinate = GetStartCoordinate();
                DigHole(startCoordinate.x,startCoordinate.y);

            }
        }

        private (int x, int y) GetStartCoordinate()
        {
            int index = UnityEngine.Random.Range(0, candidateStartingCoordinates.Count);
            var coordinate = candidateStartingCoordinates[index];
            candidateStartingCoordinates.RemoveAt(index);
            return (coordinate.x, coordinate.y);
        }

        private void SetPath(int x, int y)
        {
            maze[x, y] = MazeConfig.Path;
            if (x % 2 == 1 && y % 2 == 1)
            {
                candidateStartingCoordinates.Add((x,y));
            }
        }

        /// <summary>
        /// 外周を壁にする。
        /// </summary>
        private void CleanUp()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                    {
                        maze[x, y] = MazeConfig.Wall;
                    }
                }
            }
        }
    }
}