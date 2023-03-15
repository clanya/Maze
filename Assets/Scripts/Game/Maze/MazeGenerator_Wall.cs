using System.Collections.Generic;
using System.Diagnostics;
using Maze;

//Reference: https://algoful.com/Archive/Algorithm/MazeExtend
namespace Game.Maze
{
    class MazeGenerator_Wall : IMazeGeneratorable
    {
        private readonly int[,] maze;
        private readonly int width;
        private readonly int height;
        private List<(int x, int y)> candidateStartingCoordinates = new List<(int, int)>();
        private Stack<(int x, int y)> cullentWallCoordinates = new Stack<(int, int)>();
        public MazeGenerator_Wall(int[,] maze)
        {
            this.maze = maze;
            width = maze.GetLength(0);
            height = maze.GetLength(1);
            Debug.Assert(height % 2 == 1 && width % 2 == 1,"Set the argument to an odd number!!");
            Debug.Assert(width > MazeConfig.MinimumValue && height > MazeConfig.MinimumValue,"Set the argument to a number greater than minimum value(5)");
        }

        public void GenerateMaze()
        {
            ReadyToGenerateMaze();
            GetCandidateStartingCoordinates();
            DecideValue();
        }
    
        /// <summary>
        /// 初期化.外周はWall.他はPath.
        /// </summary>
        private void ReadyToGenerateMaze()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        maze[x, y] = MazeConfig.Wall;
                    }
                    else
                    {
                        maze[x, y] = MazeConfig.Path;
                    }
                }
            }
        }

        /// <summary>
        /// 壁伸ばしの開始位置候補の座標をリストアップ.
        /// </summary>
        private void GetCandidateStartingCoordinates()
        {
            for (int y = 0; y < height; y += 2)
            {
                for (int x = 0; x < width; x += 2)
                {
                    candidateStartingCoordinates.Add((x,y));
                }
            }

        }
    
        private void DecideValue()
        {
            while (candidateStartingCoordinates.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, candidateStartingCoordinates.Count);
                var coordinate = (candidateStartingCoordinates[index]);
                candidateStartingCoordinates.RemoveAt(index);
                int x = coordinate.x;
                int y = coordinate.y;
                if (maze[x, y] == MazeConfig.Path)
                {
                    cullentWallCoordinates.Clear();
                    ExtendWall(x, y);
                }
            }
        }
    
        private void ExtendWall(int x,int y)
        {
            var directions = new List<MazeConfig.Direction>();
            if (maze[x, y - 1] == MazeConfig.Path && !IsCurrentWall(x, y - 2))
            {
                directions.Add(MazeConfig.Direction.Up);
            }

            if (maze[x + 1, y] == MazeConfig.Path && !IsCurrentWall(x + 2, y))
            {
                directions.Add(MazeConfig.Direction.Right);
            }

            if (maze[x, y + 1] == MazeConfig.Path && !IsCurrentWall(x, y + 2))
            {
                directions.Add(MazeConfig.Direction.Down);
            }

            if (maze[x - 1, y] == MazeConfig.Path && !IsCurrentWall(x - 2, y))
            {
                directions.Add(MazeConfig.Direction.Left);
            }

            if (directions.Count > 0)
            {
                SetWall(x,y);

                bool isPath = false;
                int directionIndex = UnityEngine.Random.Range(0, directions.Count);
                switch (directions[directionIndex])
                {
                    case MazeConfig.Direction.Up:
                        isPath = (maze[x, y - 2] == MazeConfig.Path);
                        SetWall(x,--y);
                        SetWall(x,--y);
                        break;
                    case MazeConfig.Direction.Right:
                        isPath = (maze[x + 2, y] == MazeConfig.Path);
                        SetWall(++x, y);
                        SetWall(++x, y);
                        break;
                    case MazeConfig.Direction.Down:
                        isPath = (maze[x, y + 2] == MazeConfig.Path);
                        SetWall(x, ++y);
                        SetWall(x, ++y);
                        break;
                    case MazeConfig.Direction.Left:
                        isPath = (maze[x - 2, y] == MazeConfig.Path);
                        SetWall(--x, y);
                        SetWall(--x, y);
                        break;
                }

                if (isPath)
                {
                    ExtendWall(x,y);
                }

            }
            else
            {
                // すべて現在拡張中の壁にぶつかる場合、バックして再開
                var beforeCoordinate = cullentWallCoordinates.Pop();
                ExtendWall(beforeCoordinate.x,beforeCoordinate.y);
            }
        }

        private bool IsCurrentWall(int x, int y)
        {
            return cullentWallCoordinates.Contains((x, y));
        }
    
        // 壁を拡張する
        private void SetWall(int x, int y)
        {
            maze[x, y] = MazeConfig.Wall;
            if (x % 2 == 0 && y % 2 == 0)
            {
                cullentWallCoordinates.Push((x,y));
            }
        }
    
    }
}