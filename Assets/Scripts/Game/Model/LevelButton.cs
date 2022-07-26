using Game.Maze;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Model
{
    public sealed class LevelButton
    {
        private readonly MazeLevel mazeLevel;
        private const string GameObjectName = "GameSceneDirector";
        public LevelButton(MazeLevel mazeLevel)
        {
            this.mazeLevel = mazeLevel;
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
            mazeGeneratorManager.mazeLevel = mazeLevel;
        }
    }
}