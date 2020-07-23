using System;
using Managers;
using UnityEngine;

namespace EnemyScripts
{
    public class SharkController : EnemyController
    {

        public BoxCollider2D attackCollider;
        public GameObject trigger;
        public bool attackTriggered;
        public Animator sharkAnim;

        public LayerMask defaultLayer;
        public AudioClip attackClip;
        

        public override void Start()
        {
            base.Start();
        }

        public enum SharkState
        {
            Idle,
            Attack,
            Busy,
            Death,
            Ready
        }

        public SharkState sharkState;
        private static readonly int State = Animator.StringToHash("State");


        public void DetermineState()
        {
            switch (sharkState)
            {
                case SharkState.Idle:
                    sharkAnim.SetInteger(State,0);
                    break;
                case SharkState.Attack:

                    LookPlayer();
                    sharkAnim.SetInteger(State,1);
                    break;
                case SharkState.Busy:
                    break;
                case SharkState.Death:
                    gameObject.layer = defaultLayer;
                    sharkAnim.SetInteger(State,2);
                    break;
                case SharkState.Ready:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

       
        public void AttackPlayer()
        {
            PlayAttack();
            attackCollider.enabled = true;
        }
        
        public void BePassive()
        {
            
            if (enemyHealth <= 0)
            {
                
            }
            else
            {   attackCollider.enabled = false;
                sharkState = SharkState.Idle;
                attackTriggered = false;
                DetermineState();
            }
            
        }

        public void PlayAttack()
        {
            AudioManager.Manager.PlaySfx(attackClip);
        }

        public void OnSharkDeath()
        {
            Destroy(gameObject);
        }
        public override void OnDeath()
        {
            Destroy(trigger);
            sharkState = SharkState.Death;
            DetermineState();
            

        }
    }
}
