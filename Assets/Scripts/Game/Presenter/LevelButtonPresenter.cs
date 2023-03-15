using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Audio;
using Game.Maze;
using Game.Model;
using Game.View;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using AudioType = Game.Audio.AudioType;

namespace Game.Presenter
{
    public class LevelButtonPresenter : MonoBehaviour
    {
        //このPresenterは自身の責務を越権している。Jsonから取得するのは別のクラスで行うべきか？
        //lockButtons()に関してだが、本来なら各モデルがメソッドを用意し、それをPresenterでforeachで回すだけにするべき。
        //しかし、自身のinteractableをfalseにするだけならわざわざメソッドを用意する必要はないし、
        //LockButtons()を各モデルで定義するのは、それぞれのモデルが他のモデルの情報を知ることになるため
        //適切でないように思う。
        [SerializeField] private List<LevelButtonView> levelButtonViewList  = new List<LevelButtonView>();
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            ActivateAllButton(true);
            
            //それぞれのUIは自身が持っているパラメータを元にシーン遷移。index使いたいのでfor文。
            for (int i = 0; i < levelButtonViewList.Count; i++)
            {
                var level = new LevelButtonModel(new MazeLevelModel(i));
                var buttonView = levelButtonViewList[i];
                buttonView.ClickObservable() 
                    .Subscribe(async _ =>
                    {
                        ActivateAllButton(false);
                        AudioManager.Instance.PlayAsync(AudioType.se_01);
                        await buttonView.AnimationAsync(this.GetCancellationTokenOnDestroy());
                        level.MoveGameScene();
                    }).AddTo(this);
            }
        }

        private void ActivateAllButton(bool value)
        {
            foreach (var buttonView in levelButtonViewList)
            {
                buttonView.Activate(value);
            }
        }
    }
}
