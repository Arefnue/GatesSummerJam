using System;
using System.Collections;
using DG.Tweening;
using EnemyScripts;
using Managers;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerHealth : MonoBehaviour
    {

        public float health;
        //public float damage; // Bullet or MeleeAttack contains damage
        private Rigidbody2D _rb;
        private PlayerController _controller;
        private SpriteRenderer _renderer;
        public float takeNoDamageTime;
        private bool _takeNoDamage = false;
        public GameObject playerSword;
        private bool _gameOver;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _controller = GetComponent<PlayerController>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void TakeDamage(float value)
        {
            if (_takeNoDamage == true)
            {
                return;   
            }

            if (_gameOver)
            {
                return;
            }

            if (GameManager.Manager.gameOver)
            {
                return;
            }

            _renderer.DOColor(Color.red, 0.01f).OnComplete(() => { _renderer.DOColor(Color.white, 0.01f); }).SetEase(Ease.Flash);
            StartCoroutine(NoDamageRoutine());
            health -= value;

            if (health <= 0)
            {
                _gameOver = true;
                //GameManager.Manager.GameOver();
            }
        }

        public void Heal(float value)
        {
            health += value;
            
            
            _renderer.DOColor(Color.green, 0.01f).OnComplete(() => { _renderer.DOColor(Color.white, 0.01f); }).SetEase(Ease.Flash);
            if (health >= 100)
            {
                health = 100;
            }
        }

        IEnumerator NoDamageRoutine()
        {
            _takeNoDamage = true;
            yield return new WaitForSeconds(takeNoDamageTime);
            _renderer.enabled = true;
            _takeNoDamage = false;

        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && !playerSword.activeInHierarchy)
            {
                var enemyProfile = other.gameObject.GetComponent<EnemyController>();
                if (!enemyProfile)
                {
                    enemyProfile = other.gameObject.GetComponentInParent<EnemyController>();
                }

                _controller.knockbackCount = _controller.knockbackLength;

                if (other.transform.position.x < transform.position.x)
                {
                    _controller.knockFromRight = false;
                }
                else if (other.transform.position.x > transform.position.x)
                {
                    _controller.knockFromRight = true;
                }
            
                TakeDamage(enemyProfile.enemyDamage);
            }
        
        
        }
    }
}
