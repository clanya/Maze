using System.IO;
using Maze;
using UnityEngine;

namespace Utility
{
    public sealed class JsonGenerator : MonoBehaviour
    {
        private MazeGeneratorParameter mazeGeneratorParameter = new MazeGeneratorParameter();

        private void Awake()
        {
            for (int i = 0; i < 10; i++)
            {
                MazeGeneratorType mazeGeneratorType = MazeGeneratorType.Stick;
                switch (i%3)
                {
                    case 0: mazeGeneratorType = MazeGeneratorType.Hole;
                        break;
                    case 1: mazeGeneratorType = MazeGeneratorType.Stick;
                        break;
                    case 2: mazeGeneratorType = MazeGeneratorType.Wall;
                        break;
                }
                mazeGeneratorParameter.mazeGeneratorTypes.Add(mazeGeneratorType);
                mazeGeneratorParameter.widths.Add(35 + i*10);
                mazeGeneratorParameter.heights.Add(35 + i*10);
            }

            string json = JsonUtility.ToJson(mazeGeneratorParameter);
            string jsonPath = "Assets/Resources/MazeGeneratorParameterData.json";
            File.WriteAllText(jsonPath, json);
        }
    }
}