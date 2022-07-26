using System;
using Game.Player;
using UnityEngine;

namespace Game.Director
{
    public sealed class GoalDirector : MonoBehaviour
    {
        private GameObject canvas;
        private Action clearAction;

        private void Awake()
        {
            Init();
            clearAction = GameSceneDirector.Instance.ClearAction;
        }

        private void Init()
        {
            canvas = FindObjectOfType<Canvas>().gameObject;
            canvas.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent<PlayerController>(out _))
            {
                clearAction();
                canvas.SetActive(true);
            }
        }
    }
}
