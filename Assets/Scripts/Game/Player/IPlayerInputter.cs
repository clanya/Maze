namespace Game.Player
{
    public interface IPlayerInputter
    {
        public bool PutWarpPoint();
        public bool DeleteWarpPoint();
        public bool WarpToPoint();        
        public bool MoveUpward();
        public bool MoveDownward();
        public bool MoveRight();
        public bool MoveLeft();
        public bool IsRunning();
        public bool IsIdle();
        public MoveType GetMoveType();
    }
}

