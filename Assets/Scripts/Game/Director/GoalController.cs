using System;
using Cysharp.Threading.Tasks;
using Game.Player;
using UniRx;
using UnityEngine;

namespace Game.Director
{
    public sealed class GoalController : MonoBehaviour
    {
        private Subject<Unit> onTrigerEnterSubject = new Subject<Unit>();
        public IObservable<Unit> OnTriggerEnterObservable => onTrigerEnterSubject;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent<PlayerController>(out _))
            {
                onTrigerEnterSubject.OnNext(new Unit());
            }
        }

        private void OnDestroy()
        {
            onTrigerEnterSubject.Dispose();
        }
    }
}
