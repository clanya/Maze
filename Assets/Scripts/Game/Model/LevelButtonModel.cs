using Game.Maze;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Model
{
    public sealed class LevelButtonModel
    {
        private readonly MazeLevelModel mazeLevelModel;
        private const string GameObjectName = "GameSceneDirector";
        public LevelButtonModel(MazeLevelModel mazeLevelModel)
        {
            this.mazeLevelModel = mazeLevelModel;
        }
        public void MoveGameScene()
        {
            SceneManager.sceneLoaded += SetParameter;
            SceneManager.LoadScene(SceneName.GameSceneName);
        }

        private void SetParameter(Scene next, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= SetParameter;
            var mazeGeneratorManager = GameObject.Find(GameObjectName).GetComponent<MazeGeneratorManager>();
            mazeGeneratorManager.mazeLevelModel = mazeLevelModel;
        }
    }
}