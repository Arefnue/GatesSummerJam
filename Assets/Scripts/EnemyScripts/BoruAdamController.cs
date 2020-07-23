using System;
using Managers;
using UnityEngine;

namespace EnemyScripts
{
    public class BoruAdamController : EnemyController
    {
        
        public Transform attackPoint;
        public float attackRange;
        public LayerMask playerLayer;
        public float boxCastSize;
        public Animator boruAnim;
        private bool _isChecked;
        public GameObject enemyBullet;
        private static readonly int Attack = Animator.StringToHash("Attack");
        public GameObject scoreObject;
        public AudioClip attackClip;

        public override void Start()
        {
            base.Start();
            FacingRight = false;
        }

        private void Update()
        {
            if (Managers.GameManager.Manager.gameOver == true)
            {
                return;
            }
            CheckPlayer();
        }

        public void CheckPlayer()
        {
            if (_isChecked)
            {
                return;
            }

            // if (PlayerHealth.transform.position.x > transform.position.x && !FacingRight)
            // {
            //     transform.eulerAngles = new Vector3(0,-180,0);
            // }
            // else if (PlayerHealth.transform.position.x < transform.position.x && FacingRight)
            // {
            //     transform.eulerAngles = new Vector3(0,0,0);
            // }
            
            var playerHit = Physics2D.BoxCast(attackPoint.position, new Vector2(boxCastSize, boxCastSize), 0f,
                -transform.right, attackRange, playerLayer);
            
            if (playerHit)
            {
                _isChecked = true;
                boruAnim.SetTrigger(Attack);
            }
            
        }


        public void AttackToPlayer()
        {
            AudioManager.Manager.PlaySfxTunnels(attackClip);
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
        }

        public void BePassive()
        {
            _isChecked = false;
        }

        private void OnDestroy()
        {
            if (enemyHealth <= 0)
            {
                Instantiate(scoreObject, transform.position, Quaternion.identity);
            }
        }
    }
}
