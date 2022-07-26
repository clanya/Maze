using System;
using Maze;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Maze
{
    public class MazeGeneratorManager : MonoBehaviour
    {
        private BaseMazeGenerator baseMazeGenerator;
        [NonSerialized] public MazeLevel mazeLevel;
        public int[,] maze { get; private set; }
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private GameObject floorPrefab;

        /// <summary>
        /// SceneManager.sceneLoadedでデータを渡してから初期化したいので、AwakeではなくStartで初期化。
        /// </summary>
        public void Init()
        {
            maze = new int[mazeLevel.width, mazeLevel.height];
            GenerateMaze();
        }

        private void GenerateMaze()
        {
            switch (mazeLevel.mazeGeneratorType)
            {
                case MazeGeneratorType.Stick:
                    baseMazeGenerator = new MazeGenerator_Stick(maze);
                    break;
                case MazeGeneratorType.Wall:
                    baseMazeGenerator = new MazeGenerator_Wall(maze);
                    break;
                case MazeGeneratorType.Hole:
                    baseMazeGenerator = new MazeGenerator_Hole(maze);
                    break;
            }
            baseMazeGenerator.GenerateMaze();
            
            for (int x = 0; x < mazeLevel.width; x ++)
            {
                for (int y = 0; y < mazeLevel.height; y ++)
                {
                    if (maze[x, y] == MazeConfig.Wall)
                    {
                        Instantiate(wallPrefab, new Vector3(x,y,0),Quaternion.identity);
                    }
                    else if (maze[x, y] == MazeConfig.Path)
                    {
                        Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    }
                }
            }
        }
    }
}