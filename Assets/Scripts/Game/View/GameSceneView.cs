using System;
using UnityEngine;

namespace Game.View
{
    public sealed class GameSceneView : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void Awake()
        {
            SetViewActive(false);
        }

        public void SetViewActive(bool value)
        {
            canvas.SetActive(value);
        }
    }
}