using System;
using System.Collections;
using DG.Tweening;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public abstract class EnemyController : MonoBehaviour
    {

        public float enemyHealth;
        public float enemyDamage;
        protected PlayerHealth PlayerHealth;
        protected bool FacingRight;
        private SpriteRenderer _renderer;
        protected Rigidbody2D EnemyRb;
        protected Animator EnemyAnimator;
        protected Managers.GameManager GameManager;

        public GameObject swordHitParticle;
        public GameObject deathParticle;
        public GameObject bulletParticle;

        public virtual void Start()
        {
            EnemyRb = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            PlayerHealth = FindObjectOfType<PlayerHealth>();
            EnemyAnimator = GetComponent<Animator>();
            GameManager = FindObjectOfType<Managers.GameManager>();

            // if( !EnemyRb  || !_renderer || !PlayerHealth || !EnemyAnimator || !GameManager)
            // {
            //     throw new Exception("Scene does not contain expected Object");
            // }
        }


        public virtual void TakeDamage(float value)
        {
            enemyHealth -= value;

            _renderer.DOColor(Color.red, 0.01f).OnComplete(() => { _renderer.DOColor(Color.white, 0.01f); }).SetEase(Ease.Flash);
            
            if (enemyHealth <=0)
            {
                OnDeath();    
            }
        }

        public virtual void OnDeath()
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        protected virtual void LookPlayer()
        {
            if (PlayerHealth.transform.position.x < transform.position.x && FacingRight)
            {
                Flip();
            }
            else if (PlayerHealth.transform.position.x > transform.position.x && !FacingRight)
            {
                Flip();
            }

        }

        protected void Flip()
        {
            FacingRight = !FacingRight;
            var transform1 = transform;
            var scale = transform1.localScale;
            scale.x *= -1;
            transform1.localScale = scale;
        }


        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                Destroy(other.gameObject);
                Instantiate(bulletParticle, transform.position, Quaternion.identity);
                TakeDamage(other.gameObject.GetComponent<BulletController>().damage);
            }

            if (other.CompareTag("Sword"))
            {
                Instantiate(swordHitParticle, transform.position, Quaternion.identity);
                TakeDamage(other.gameObject.GetComponent<PlayerMeleeAttack>().damage);
            }
        }
    }
}
