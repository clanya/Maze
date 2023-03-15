using System.IO;
using Maze;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Debug用で雑にデータを作成するために使用する。
    /// </summary>
    public sealed class JsonGenerator : MonoBehaviour
    {
        private MazeGeneratorParameter mazeGeneratorParameter = new MazeGeneratorParameter();
        private const int DataCount = 10;                                                               //生成したいデータ数
        private const string GeneratedJsonPath = "Assets/Resources/MazeGeneratorParameterData.json";    //生成させるパス
        private void Awake()
        {
            for (int i = 0; i < DataCount; i++)
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
                mazeGeneratorParameter.widths.Add(35 + i * DataCount);
                mazeGeneratorParameter.heights.Add(35 + i * DataCount);
            }

            string json = JsonUtility.ToJson(mazeGeneratorParameter);
            File.WriteAllText(GeneratedJsonPath, json);
        }
    }
}