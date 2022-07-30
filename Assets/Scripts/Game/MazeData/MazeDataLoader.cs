using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Maze
{
    public sealed class MazeDataLoader
    {
        private readonly List<MazeLevel> mazeLevelList;
        private const string DataPath = "MazeGeneratorParameterData";

        public MazeDataLoader()
        {
            mazeLevelList = new List<MazeLevel>();
        }

        public List<MazeLevel> LoadMazeLevelList()
        {
            //Level情報を受け取り、mazeLevelListに格納。
            foreach (var mazeLevel in TryGetMazeLevelData())
            {
                if (mazeLevel.width < 0)
                {
                    throw new Exception($"invalid width: {mazeLevel.width}");
                }

                if (mazeLevel.height < 0)
                {
                    throw new Exception($"invalid height: {mazeLevel.height}");
                }

                mazeLevelList.Add(mazeLevel);
            }

            return mazeLevelList;
        }

        //Memo: この処理の仕方問題ありそう。nullが見つけにくいとか。
        private static MazeLevel[] TryGetMazeLevelData()
        {
            //JsonからLevel情報を読み込むのをtryする。
            try
            {
                var dataFile = Resources.Load<TextAsset>(DataPath);
                var levelTable = JsonUtility.FromJson<MazeLevelTable>(dataFile.ToString());
                return levelTable.data_table;
            }
            catch (Exception e)
            {
                throw new Exception($"data load error: {e}");
            }
        }
    }
}