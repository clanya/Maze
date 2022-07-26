using UnityEngine;

namespace Game.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioData), menuName = "ScriptableObjects/AudioData" )]
    public sealed class AudioData: ScriptableObject
    {
        [SerializeField] private AudioType type;
        [SerializeField] private AudioClip audioClip;

        public AudioType Type => type;
        public AudioClip Clip => audioClip;
    }
}