using UnityEngine;

namespace Game.Player
{
    public class Inputter_Keyboard : IPlayerInputter
    {
        readonly KeySetting keySetting = new KeySetting();

        public bool PutWarpPoint() => Input.GetKeyDown(keySetting.PutWarpPointKey);
        public bool DeleteWarpPoint() => Input.GetKeyDown(keySetting.DeleteWarpPointKey);
        public bool WarpToPoint() => Input.GetKeyDown(keySetting.WarpKey);
        public bool MoveUpward() => Input.GetKey(keySetting.MoveUpwardKey);
        public bool MoveDownward() => Input.GetKey(keySetting.MoveDownwardKey);
        public bool MoveRight() => Input.GetKey(keySetting.MoveRightKey);
        public bool MoveLeft() => Input.GetKey(keySetting.MoveLeftKey);
        public bool IsRunning() => Input.GetKey(keySetting.IsRunningKey);
    }
}

