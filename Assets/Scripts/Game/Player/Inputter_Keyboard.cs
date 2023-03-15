using UnityEngine;

namespace Game.Player
{
    public class Inputter_Keyboard : IPlayerInputter
    {
        readonly KeySetting keySetting = new KeySetting();

        public bool MoveUpward() => Input.GetKey(keySetting.MoveUpwardKey);
        public bool MoveDownward() => Input.GetKey(keySetting.MoveDownwardKey);
        public bool MoveRight() => Input.GetKey(keySetting.MoveRightKey);
        public bool MoveLeft() => Input.GetKey(keySetting.MoveLeftKey);
        public bool IsRunning() => Input.GetKey(keySetting.IsRunningKey);
        public bool IsIdle() => !MoveUpward() && !MoveDownward() && !MoveRight() && !MoveLeft();
        public MoveType GetMoveType()
        {
            if (MoveUpward())   return MoveType.Up;
            if (MoveDownward()) return MoveType.Down;
            if (MoveLeft())     return MoveType.Left;
            if (MoveRight())    return MoveType.Right;
            return MoveType.Idle;
        }
    }
}

