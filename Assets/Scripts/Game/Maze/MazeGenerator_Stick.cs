using Maze;
using System.Diagnostics;

namespace Game.Maze
{
    //Reference: https://algoful.com/Archive/Algorithm/MazeBar
    public class MazeGenerator_Stick : BaseMazeGenerator
    {
        private readonly int[,] maze;
        private readonly int width;
        private readonly int height;

        public MazeGenerator_Stick(int[,] maze)
        {
            this.maze = maze;
            width = maze.GetLength(0);
            height = maze.GetLength(1);
            Debug.Assert(height % 2 == 1 && width % 2 == 1,"Set the argument to an odd number!!");
            Debug.Assert(width > MazeConfig.MinimumValue && height > MazeConfig.MinimumValue,"Set the argument to a number greater than minimum value(5)");
        }

        private MazeGenerator_Stick() { }
        
        public override void GenerateMaze()
        {
            // if (width % 2 == 0 || height % 2 == 0) Debug.LogError("Set the argument to an odd number!!");
            // if (width < MinimumValue || height < MinimumValue) Debug.LogError("Set the argument to a number greater than minimum value(5)");
            //ŠO•Ç‚ð•Ç‚É‚·‚é
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        maze[x, y] = MazeConfig.Wall;
                    else
                        maze[x, y] = MazeConfig.Path;

            //–_‚ð—§‚ÄA“|‚·
            var rnd = new System.Random();
            for(int x = 2;x < width - 1;x += 2)
            {
                for(int y = 2;y < height -1;y += 2)
                {
                    maze[x, y] = MazeConfig.Wall;

                    while (true)
                    {
                        int direction;
                        if (y == 2) 
                            direction = rnd.Next(4);
                        else 
                            direction = rnd.Next(3);

                        //–_‚ð“|‚·•ûŒü‚ðŒˆ‚ß‚é
                        int wallX = x;
                        int wallY = y;
                        switch (direction)
                        {
                            case (int)MazeConfig.Direction.Right:
                                wallX++;
                                break;
                            case (int)MazeConfig.Direction.Down:
                                wallY++;
                                break;
                            case (int)MazeConfig.Direction.Left:
                                wallX--;
                                break;
                            case (int)MazeConfig.Direction.Up:
                                wallY--;
                                break;
                        }

                        if(maze[wallX,wallY] != MazeConfig.Wall)
                        {
                            maze[wallX, wallY] = MazeConfig.Wall;
                            break;
                        }
                    }
                }
            }
        }
    }
}
