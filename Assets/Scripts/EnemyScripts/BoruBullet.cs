using System;
using DG.Tweening;
using UnityEngine;

namespace EnemyScripts
{
    public class BoruBullet : EnemyController
    {

        public float bulletSpeed;
        public override void Start()
        {
            base.Start();
            FacingRight = false;
        }


        private void Update()
        {
            
            if (PlayerHealth.transform.position.x > transform.position.x && !FacingRight)
            {
                transform.eulerAngles = new Vector3(0,-180,0);
            }
            else if (PlayerHealth.transform.position.x < transform.position.x && FacingRight)
            {
                transform.eulerAngles = new Vector3(0,0,0);
            }
            
            transform.DOMove(PlayerHealth.transform.position, bulletSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                Debug.Log("Puff");
            });
        }


        private void BulletDeath()
        {
            Destroy(gameObject);
        }
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (other.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }

            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            
        }
    }
}
