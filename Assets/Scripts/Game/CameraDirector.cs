using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Director;
using Game.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game
{
    public class CameraDirector : MonoBehaviour
    {
        private Transform playerTransform;
        private Vector2 goalPosition;
        [SerializeField] private float speed = 10;
        [SerializeField] private Transform cameraTransform;
        private CancellationToken token;

        private void Awake()
        {
            token = this.GetCancellationTokenOnDestroy();
        }

        private async void Start()
        {
            var playerController = FindObjectOfType<PlayerController>();
            playerTransform = playerController.GetComponent<Transform>();
            var goalPosition = FindObjectOfType<GoalController>().GetComponent<Transform>().position;
            playerController.SetCanMove(false);
            await StartAnimationAsync(playerTransform,goalPosition,token);
            playerController.SetCanMove(true);
            PlayerPositionObservable();
        }

        //cameraをGoal→Playerにむかうアニメーション
        private async UniTask StartAnimationAsync(Transform playerTransform,Vector2 goalPosition,CancellationToken token)
        {
            cameraTransform.position = new Vector3(goalPosition.x,goalPosition.y,transform.position.z);
            await UniTask.Delay(1000, cancellationToken: token);
            
            while (cameraTransform.position.x != playerTransform.position.x && cameraTransform.position.y != playerTransform.position.y)    //CameraのPosition.xyがplayerのposition.xyとい一致するまで回す。
            {
                var tmp = Vector2.MoveTowards(cameraTransform.position, playerTransform.position, speed * Time.deltaTime);
                cameraTransform.position = new Vector3(tmp.x,tmp.y,cameraTransform.position.z);
                await UniTask.Yield(PlayerLoopTiming.Update,token);
            }
        }

        //cameraをplayerに追従させる
        private void PlayerPositionObservable()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var position = playerTransform.position;
                    cameraTransform.position = new Vector3(position.x, position.y, cameraTransform.position.z);
                });
        }
    }
}
