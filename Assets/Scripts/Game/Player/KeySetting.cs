using UnityEngine;

namespace Game.Player
{
    public class KeySetting
    {
        public KeyCode PutWarpPointKey { get; private set; } = KeyCode.Space;
        public KeyCode DeleteWarpPointKey { get; private set; } = KeyCode.Space;
        public KeyCode WarpKey { get; private set; } = KeyCode.R;
        public KeyCode MoveUpwardKey { get; private set; } = KeyCode.W;
        public KeyCode MoveDownwardKey { get; private set; } = KeyCode.S;
        public KeyCode MoveRightKey { get; private set; } = KeyCode.D;
        public KeyCode MoveLeftKey { get; private set; } = KeyCode.A;
        public KeyCode IsRunningKey { get; private set; } = KeyCode.LeftShift;
    }
}