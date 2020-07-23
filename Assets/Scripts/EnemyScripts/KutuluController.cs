using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
    public class KutuluController : EnemyController
    {
        public float speed;
        public float distance;

        private bool _moveRight;

        public Transform groundDetect;
        public LayerMask groundLayer;

        public List<AudioClip> hitClipList;

        private bool _isKutulu = true;
        
        private void Update()
        {
            if (Managers.GameManager.Manager.gameOver == true)
            {
                return;
            }
            
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
            var groundInfo = Physics2D.Raycast(groundDetect.position,Vector2.down, distance,groundLayer);
            if (groundInfo.collider == false)
            {
                if (_moveRight == true)
                {
                    transform.eulerAngles = new Vector3(0,-180,0);
                    _moveRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0,0,0);
                    _moveRight = true;
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (other.CompareTag("Player") && _isKutulu)
            {
                var random = Random.Range(0, hitClipList.Count);
                
                AudioManager.Manager.PlaySfx(hitClipList[random]);
            }
        }
    }
}
