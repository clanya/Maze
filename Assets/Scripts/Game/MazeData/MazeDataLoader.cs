using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Maze
{
    public sealed class MazeDataLoader
    {
        private readonly MazeLevel[] mazeLevelList;
        private const string DataPath = "MazeGeneratorParameterData";

        public MazeDataLoader()
        {
            mazeLevelList = TryGetMazeLevelData();
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

        public MazeLevel FindMazeLevel(MazeLevelModel levelModel)
        {
            try
            {
                var mazeLevel = mazeLevelList[levelModel.mazeLevel];

                if (mazeLevel.width < 0)
                {
                    throw new Exception($"invalid width: {mazeLevel.width}");
                }

                if (mazeLevel.height < 0)
                {
                    throw new Exception($"invalid height: {mazeLevel.height}");
                }

                return mazeLevel;
            }
            catch (Exception e)
            {
                throw new Exception($"find maze error: {e}");
            }
        }
    }
}