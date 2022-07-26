using System;

namespace Game.Player
{
    public enum PlayerAnimatorFrag
    {
        isUpward,
        isDownward,
        isRight,
        isLeft,
        isIdle,
        isClear
    }

    public static class AssociatedEnumExtension
    {
        public static string ConvertToStringType(this PlayerAnimatorFrag flag)
        {
            return flag switch
            {
                PlayerAnimatorFrag.isUpward => ConstValues.IsUpward,
                PlayerAnimatorFrag.isDownward => ConstValues.IsDownward,
                PlayerAnimatorFrag.isRight => ConstValues.IsRight,
                PlayerAnimatorFrag.isLeft => ConstValues.IsLeft,
                PlayerAnimatorFrag.isIdle => ConstValues.IsIdle,
                PlayerAnimatorFrag.isClear => ConstValues.IsClear,
                _ => throw new ArgumentOutOfRangeException(nameof(flag), flag, null)
            };
        }
    }

    public static class ConstValues
    {
        public const string IsUpward = "isUpward";
        public const string IsDownward = "isDownward";
        public const string IsRight = "isRight";
        public const string IsLeft = "isLeft";
        public const string IsIdle = "isIdle";
        public const string IsClear = "isClear";
        
    }
}