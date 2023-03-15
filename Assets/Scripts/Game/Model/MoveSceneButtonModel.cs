using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using AudioType = Game.Audio.AudioType;

namespace Game.Model
{
    public sealed class MoveSceneButtonModel
    {
        public void MoveScene(string nextSceneName)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        
        public async UniTaskVoid MoveScene(string nextSceneName,CancellationToken token)
        {
            await AudioManager.Instance.PlayAsync(AudioType.se_01,token);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}