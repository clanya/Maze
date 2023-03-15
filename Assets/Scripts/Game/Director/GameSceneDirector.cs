using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Maze;
using Game.Player;
using Game.View;
using UnityEngine;
using UniRx;

namespace Game.Director
{
    public sealed class GameSceneDirector : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject goalFragPrefab;
        [SerializeField] private GameSceneView gameSceneView;
        [SerializeField] private MazeGeneratorManager mazeGeneratorManager;

        private PlayerController playerController;
        private GoalController goalController;
        
        private Vector3 PlayerStartPosition => GetPlayerStartPosition();
        private Vector3 GoalFragPosition => GetGoalFlagPosition();
        private Transform playerTransform;

        private const int PositionOffset = 2;
        private const float MoveToGoalAnimationSpeed = 0.01f;   //ゴールに近づくときの移動スピード
        
        private void Start()
        {
            mazeGeneratorManager.Init();
            Init();
        }

        private void Init()
        {
            var player = Instantiate(playerPrefab, PlayerStartPosition, Quaternion.identity);
            playerController = player.GetComponent<PlayerController>();
            playerTransform = player.GetComponent<Transform>();
            
            var goal = Instantiate(goalFragPrefab, GoalFragPosition, Quaternion.identity);
            goalController = goal.GetComponent<GoalController>();
            
            //購読開始
            goalController.OnTriggerEnterObservable
                .Take(1)
                .Subscribe(_ =>
                {
                    ClearActionAsync().Forget();
                }).AddTo(this);
        }

        //Playerの初期位置は左上
        private Vector3 GetPlayerStartPosition()
        {
            var maze = mazeGeneratorManager.maze;
            return new Vector3(1, maze.GetLength(1) - PositionOffset, 0);
        }

        //Goalの位置は右下
        private Vector3 GetGoalFlagPosition()
        {
            var maze = mazeGeneratorManager.maze;
            return new Vector3(maze.GetLength(0) - PositionOffset, 1, 0);
        }

        private async UniTask ClearActionAsync()
        {
            var token = playerTransform.gameObject.GetCancellationTokenOnDestroy();
            gameSceneView.SetViewActive(true);
            playerController.SetCanMove(false);
            await GoalAnimationAsync(token);
            playerController.SetClearAnimation();
        }

        private async UniTask GoalAnimationAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() =>
            {
                var direction = GoalFragPosition - playerTransform.position;
                playerTransform.position += direction.normalized * MoveToGoalAnimationSpeed;
                return Vector3.Distance(playerTransform.position, GoalFragPosition) < 0.05f;
            },cancellationToken : token);
        }
    }
}