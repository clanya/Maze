namespace Game.Maze
{
    public static class MazeConfig
    {
        public enum Direction
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3
        }

        public const int Path = 0;
        public const int Wall = 1;
        public const int MinimumValue = 5;

    }
}