using System;
using System.Collections.Generic;
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

        private readonly List<LevelButton> levelButtonList = new List<LevelButton>();
        private readonly List<MazeLevel> mazeLevelList = new List<MazeLevel>();
        private const string DataPath = "MazeGeneratorParameterData";
    
        private void Awake()
        {
            Init();
        }

        //JsonUtilityが生の配列を扱うことができないので、とりあえずダミーのclassでラップしたが、拡張メソッドなりで実装したほうがいいだろう。
        //参照：https://takap-tech.com/entry/2021/02/02/222406
        private void Init()
        {
            foreach (var levelButtonView in levelButtonViewList)
            {
                levelButtonView.GetComponent<Button>().enabled = true;
            }
            
            //Level情報を受け取り、mazeLevelListに格納。
            foreach (var mazeLevel in TryGetMazeLevelData())
            {
                if (mazeLevel.width < 0)
                {
                    throw new Exception($"invalid width: {mazeLevel.width}");
                }

                if (mazeLevel.height < 0)
                {
                    throw new Exception($"invalid height: {mazeLevel.height}");
                }

                mazeLevelList.Add(mazeLevel);
            }


            //mazeLevelList（Model(LevelButtonクラス型List)）にindexで対応づくようにmazeLevelを追加する。
            foreach (var mazeLevel in mazeLevelList)
            {
                levelButtonList.Add(new LevelButton(mazeLevel));
            }

            //それぞれのUIは自身が持っているパラメータを元にシーン遷移。index使いたいのでfor文。
            for (int i = 0; i < levelButtonViewList.Count; i++)
            {
                var tmp = levelButtonList[i];
                var tmp2 = levelButtonViewList[i];
                levelButtonViewList[i].GetComponent<Button>().OnClickAsObservable()
                    .Subscribe(async _ =>
                    {
                        LockButtons();
                        // var token = new CancellationToken();
                        AudioManager.Instance.Play(AudioType.se_01);
                        await tmp2.Animation(this.GetCancellationTokenOnDestroy());
                        tmp.MoveGameScene();
                    }).AddTo(this);
            }
        }

        //Memo: この処理の仕方問題ありそう。nullが見つけにくいとか。
        private MazeLevel[] TryGetMazeLevelData()
        {
            //JsonからLevel情報を読み込むのをtryする。
            try
            {
                var dataFile = Resources.Load<TextAsset>(DataPath);
                var levelTable = JsonUtility.FromJson<MazeLevelTable>(dataFile.ToString());
                return levelTable.data_table;
            }
            catch(Exception e)
            {
                throw new Exception($"data load error: {e}");
            }
        }

        private void LockButtons()
        {
            foreach(var button in levelButtonViewList)
            {
                //button.GetComponent<Button>().interactable = false;
                button.GetComponent<Button>().enabled = false;
            }
        }
    }
}
