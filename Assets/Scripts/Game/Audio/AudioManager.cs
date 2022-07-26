using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;

namespace Game.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        [SerializeField] private List<AudioData> audioDatas;
        public AudioSource audioSource { get; private set; }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioType type)
        {
            AudioClip audioClip = FindAudioClip(type);
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public async UniTask Play(AudioType type,CancellationToken token)
        {
            AudioClip audioClip = FindAudioClip(type);
            audioSource.clip = audioClip;
            audioSource.Play();
            await UniTask.WaitUntil(() => audioSource.isPlaying == false, cancellationToken: token);
        }

        public void PlayOneShot(AudioType type)
        {
            AudioClip audioClip = FindAudioClip(type);
            audioSource.PlayOneShot(audioClip);
        }

        private AudioClip FindAudioClip(AudioType type)
        {
            return audioDatas.Find(x => x.Type == type).Clip;
        }
    }
}