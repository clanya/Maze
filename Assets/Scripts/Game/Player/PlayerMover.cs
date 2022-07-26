using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMover
    {
        private readonly Rigidbody2D rigidbody;
        private const int WalkSpeed = 150;

        public PlayerMover(Rigidbody2D rigidbody2D)
        {
            rigidbody = rigidbody2D;
        }
        
        public void WalkUpward() => rigidbody.velocity = Vector2.up * Time.deltaTime * WalkSpeed;
        
        public void WalkDownward() => rigidbody.velocity = Vector2.down * Time.deltaTime * WalkSpeed;
        
        public void WalkRight() => rigidbody.velocity = Vector2.right * Time.deltaTime * WalkSpeed;

        public void WalkLeft() => rigidbody.velocity = Vector2.left * Time.deltaTime * WalkSpeed;

        public void Idle() => rigidbody.velocity = Vector2.zero;

    }
}
