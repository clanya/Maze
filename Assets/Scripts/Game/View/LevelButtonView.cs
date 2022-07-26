using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public sealed class LevelButtonView : MonoBehaviour
    {
        private Button button;
        private RectTransform rectTransform;
        private const float MaxSize = 1.1f;
        private const int DelaySpan = 100;
        private const float Speed = 0.02f;
    
        private void Awake()
        {
            button = gameObject.GetComponent<Button>();
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        public async UniTask Animation(CancellationToken token)
        {
            while (rectTransform.localScale.y <= MaxSize)
            {
                await UniTask.Delay(DelaySpan,cancellationToken:token);
                    var localScale = rectTransform.localScale;
                    localScale = new Vector3(localScale.x, localScale.y + Speed, localScale.z);
                    rectTransform.localScale = localScale;
            }
            await UniTask.Delay(500, cancellationToken: token);
        }
    }
}