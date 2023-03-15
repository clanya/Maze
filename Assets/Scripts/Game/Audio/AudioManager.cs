using System;
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
        [SerializeField] private List<AudioData> audioDataList;
        public AudioSource audioSource { get; private set; }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAsync(AudioType type)
        {
            var audioClip = FindAudioClip(type);
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public async UniTask PlayAsync(AudioType type,CancellationToken token)
        {
            var audioClip = FindAudioClip(type);
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
            var data =  audioDataList.Find(x => x.Type == type);
            if (data is null)
            {
                throw new Exception($"Audio data is null. (type: {type})");
            }

            if (data.Clip is null)
            {
                throw new Exception($"Audio clip is null. (type: {type})");
            }

            return data.Clip;
        }
    }
}