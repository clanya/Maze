using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Maze;
using UnityEngine;
using Utility;

namespace Game.Director
{
    public sealed class GameSceneDirector : SingletonMonoBehaviour<GameSceneDirector>
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject goalFragPrefab;
        [SerializeField] private MazeGeneratorManager mazeGeneratorManager;
        private Vector3 PlayerStartPosition => GetPlayerStartPosition();
        private Vector3 GoalFragPosition => GetGoalFlagPosition();
        private const int PositionOffset = 2;
        private const float MaxDistanceDelta = 0.3f;
        public bool IsFinished { private set; get; } = false;
        private Transform playerTransform;
        
        private void Start()
        {
            mazeGeneratorManager.Init();
            Init();
        }

        private void Init()
        {
            playerTransform = Instantiate(playerPrefab, PlayerStartPosition, Quaternion.identity).GetComponent<Transform>();
            Instantiate(goalFragPrefab, GoalFragPosition, Quaternion.identity);
        }

        private Vector3 GetPlayerStartPosition()
        {
            var maze = mazeGeneratorManager.maze;
            return new Vector3(1, maze.GetLength(1) - PositionOffset, 0);
        }

        private Vector3 GetGoalFlagPosition()
        {
            var maze = mazeGeneratorManager.maze;
            return new Vector3(maze.GetLength(0) - PositionOffset, 1, 0);
        }

        public void ClearAction()
        {
            var token = playerTransform.gameObject.GetCancellationTokenOnDestroy();
            IsFinished = true;
            GoalAnimation(token).Forget();
            // playerTransform.position = Vector3.MoveTowards(playerTransform.position, GetGoalFlagPosition(), MaxDistanceDelta);
        }

        private async UniTaskVoid GoalAnimation(CancellationToken token)
        {
            await UniTask.WaitUntil(() =>
            {
                var direction = GoalFragPosition - playerTransform.position;
                playerTransform.position += direction.normalized * 0.01f;
                return Vector3.Distance(playerTransform.position, GoalFragPosition) < 0.05f;
            });
        }
    }
}