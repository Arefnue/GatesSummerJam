using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;


namespace EnemyScripts
{
    public class FrogBoss : EnemyController
    {
        [Header("Config")]
        public float tongueLength = 9f;
        public float jumpMaxDistance = 4f;

        [Header("Decision Config")]
        [Range(0, 9)]
        public int jumpPossibility = 1;
        [Range(0, 5)][Tooltip("Slam will land on player's position with -+(maxSlamError) ")]
        public float maxSlamError = 3f;
        [Range(2, 10)][Tooltip("This value will be used to restrict same number of consequtive actions")]
        public int allowedConsequtiveActions = 2;

        public GameObject scoreObject;
        
        public enum FrogState
        {
            Idle,
            Slam,
            SlamUp,
            SlamDown,
            Jump,
            JumpUp,
            TongueAttack,
        }

        // States
        [SerializeField] private bool isSleeping = true;
        private FrogState state = FrogState.Idle;
        private Vector2 slamTargetCoord, jumpTargetCoord;
        private List<string> lastActions = new List<string>();
        private Vector3 _basePos;
        public Action onDeath;
        public AudioClip slamClip;
        public AudioClip jumpClip;
        public AudioClip tongueClip;

        public override void Start()
        {
            base.Start();
            _basePos = transform.position;
        }

        public void WakeFrog()
        {
            isSleeping = false;
        }

        public void SleepFrog()
        {
            isSleeping = true;
            transform.position = _basePos;
        }

        public void PlaySlam()
        {
            AudioManager.Manager.PlaySfxTunnels(slamClip);
        }

        public void PlayTongue()
        {
            AudioManager.Manager.PlaySfxTunnels(tongueClip);
        }

        public void PlayJump()
        {
            AudioManager.Manager.PlaySfxTunnels(jumpClip);
        }
        
        private void PerformAction()
        {
            if (isSleeping) { return; }
            string trigger = "Slam";
            var xDistance = Mathf.Abs(XDistanceFromPlayer());

            if (xDistance < tongueLength)
            {
                trigger = "Tongue";
            }
            else if ( xDistance < tongueLength + jumpMaxDistance / UnityEngine.Random.Range(1f, 2f) || jumpPossibility >= UnityEngine.Random.Range(0, 10) )
            {
                trigger = "Jump";
            }

            trigger = CheckConsequtiveAction(trigger);

            EnemyAnimator.SetTrigger(trigger);
        }

        private string CheckConsequtiveAction(string trigger)
        {
            bool isSame = true;
            for (int it = lastActions.Count - 2; it >= 0 && it >= lastActions.Count - allowedConsequtiveActions; it--)
            {
                if(lastActions[it] != lastActions[it + 1])
                {
                    isSame = false;
                    break;
                }
            }

            if (isSame)
            {
                var pool = new List<string>() { "Slam", "Tongue", "Jump" };
                pool.Remove(trigger);
                int randomActionIndex = UnityEngine.Random.Range(0, pool.Count);
                trigger = pool[randomActionIndex];
            }

            lastActions.Add(trigger);

            return trigger;
        }

        public void ChangeFrogState(FrogState newState)
        {
            state = newState;

            switch (state)
            {
                // Go vertically up above camera
                // And horizontally half of the difference
                case FrogState.SlamUp:
                    CheckAndFlip();
                    HandleSlamUp();
                    break;

                // Go to players position where initially SlamUp was triggered
                case FrogState.SlamDown:
                    HandleSlamDown();
                    break;

                case FrogState.Slam:
                    var targetX = PlayerHealth.transform.position.x + UnityEngine.Random.Range(-maxSlamError, maxSlamError);
                    slamTargetCoord = new Vector2(targetX, transform.position.y);

                    break;
                case FrogState.Jump:
                    var addX = Mathf.Sign(XDistanceFromPlayer()) * jumpMaxDistance;
                    jumpTargetCoord = new Vector2(transform.position.x + addX, transform.position.y);
                    break;

                case FrogState.JumpUp:
                    HandleJumpUp();
                    break;

                case FrogState.Idle:
                    CheckAndFlip();
                    PerformAction();
                    break;

                case FrogState.TongueAttack:
                    /* Nothing */
                    break;
            }
        }

        private void HandleJumpUp()
        {
            EnemyRb.DOMove(jumpTargetCoord, 0.5f);
        }

        private void HandleSlamUp()
        {
            var cameraHeight = Camera.main.orthographicSize * 2f;

            var xDif = slamTargetCoord.x - transform.position.x;
            var xMove = xDif / 2 + transform.position.x; // Horizontally go half of the position dif
            var yMove = cameraHeight + transform.position.y;

            EnemyRb.DOMove(new Vector2(xMove, yMove), 0.5f);
        }

        private void HandleSlamDown()
        {
            var cameraHeight = Camera.main.orthographicSize * 2f;

            var xMove = slamTargetCoord.x;
            var yMove = slamTargetCoord.y;

            EnemyRb.DOMove(new Vector2(xMove, yMove), 0.5f);


        }
        

        private void CheckAndFlip()
        {
            var xDistance = XDistanceFromPlayer();
            var localScale = transform.localScale;

            if (xDistance > 0)
            {
                localScale.x = Mathf.Abs(transform.localScale.x) * -1;
            }
            else
            {
                localScale.x = Mathf.Abs(transform.localScale.x);
            }

            transform.localScale = localScale;
        }

        private float XDistanceFromPlayer()
        {
            return PlayerHealth.transform.position.x - transform.position.x;
        }
        
        
        private void OnDestroy()
        {
            
            if (enemyHealth <= 0)
            {
                onDeath?.Invoke();

                Instantiate(scoreObject, transform.position, Quaternion.identity);
            }
           
            //GameManager.collectedSoulCount++;
        }
    }
}
