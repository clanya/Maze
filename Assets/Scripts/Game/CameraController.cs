using Game.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        private Transform playerTransform;

        private void Start()
        {
            playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var position = playerTransform.position;
                    transform.position = new Vector3(position.x, position.y, transform.position.z);
                });
        }
    }
}
