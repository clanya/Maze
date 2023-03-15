using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMover
    {
        private readonly Rigidbody2D rigidbody;
        private int WalkSpeed = 5;

        public PlayerMover(Rigidbody2D rigidbody2D)
        {
            rigidbody = rigidbody2D;
        }
        
        public void Move(MoveType moveType)
        {
            rigidbody.velocity = moveType switch
            {
                MoveType.Idle  => Vector2.zero,
                MoveType.Up    => WalkSpeed * Vector2.up,
                MoveType.Down  => WalkSpeed * Vector2.down,
                MoveType.Left  => WalkSpeed * Vector2.left,
                MoveType.Right => WalkSpeed * Vector2.right,
                _ => throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null)
            };
        }
    }
}
