using UnityEngine;
using Game.Audio;
using Game.Director;
using AudioType = Game.Audio.AudioType;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        private IPlayerInputter playerInputter;
        private PlayerMover playerMover;
        private Animator playerAnimator;
        private bool isIdle;

        private Vector3 positionBuffer;
        // private GameObject warpHoleGameObject;
        protected void Awake()
        {
            playerInputter = new Inputter_Keyboard();
            playerMover = new PlayerMover(GetComponent<Rigidbody2D>());
            playerAnimator = GetComponent<Animator>();
        }
        
        private void FixedUpdate()
        {
            Move();
        }
        
        private void Move()
        {
            if (GameSceneDirector.Instance.IsFinished)
            {
                playerAnimator.SetBool(PlayerAnimatorFrag.isClear.ToString(),true);
                return;
            }

            isIdle = false;
            playerAnimator.SetBool(PlayerAnimatorFrag.isUpward.ConvertToStringType(),playerInputter.MoveUpward());
            playerAnimator.SetBool(PlayerAnimatorFrag.isDownward.ConvertToStringType(),playerInputter.MoveDownward());
            playerAnimator.SetBool(PlayerAnimatorFrag.isRight.ConvertToStringType(),playerInputter.MoveRight());
            playerAnimator.SetBool(PlayerAnimatorFrag.isLeft.ConvertToStringType(),playerInputter.MoveLeft());
            playerAnimator.SetBool(PlayerAnimatorFrag.isIdle.ConvertToStringType(),isIdle);
            
            if (playerInputter.MoveUpward())
            {
                playerMover.WalkUpward();
            }
            else if (playerInputter.MoveDownward())
            {
                playerMover.WalkDownward();
            }
            else if (playerInputter.MoveRight())
            {
                playerMover.WalkRight();
            }
            else if (playerInputter.MoveLeft())
            {
                playerMover.WalkLeft();
            }
            else
            {
                isIdle = true;
                playerAnimator.SetBool(PlayerAnimatorFrag.isIdle.ToString(),true);
                playerMover.Idle();
            }

            //このままだとPause()を呼ばれたときに区別がつかない。
            //前フレームと比較して動いていたら音を鳴らす。
            //Memo:公開用はAudioType.se_02を削除しているのでコメントアウト
            if (!isIdle && !AudioManager.Instance.audioSource.isPlaying && transform.position != positionBuffer)
            {
                // AudioManager.Instance.Play(AudioType.se_02);
            }
            positionBuffer = transform.position;
        }
    }
}
