using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Audio;
using Game.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Game.Audio.AudioType;

namespace Game.Presenter
{
    public sealed class BackToTitleButtonPresenter : MonoBehaviour
    {
        [SerializeField] private Button button;
        private readonly MoveSceneButton moveSceneButton = new MoveSceneButton();
        private const float MaxSize = 1.1f;
        private const int DelaySpan = 100;
        private const float Speed = 0.02f;

        private void Awake()
        {
            var token = this.GetCancellationTokenOnDestroy();
            button.OnClickAsObservable()
                .Take(1)
                .Subscribe( async _ =>
                {
                    button.interactable = false;
                    AudioManager.Instance.Play(AudioType.se_01);
                    await Animation(token);
                    moveSceneButton.MoveScene(SceneName.TitleSceneName);
                    // moveSceneButton.MoveScene(SceneName.TitleSceneName, token).Forget();
                }).AddTo(this);
        }
        
        public async UniTask Animation(CancellationToken token)
        {
            var rectTransform = button.GetComponent<RectTransform>();
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