using System;
using PlayerScripts;
using UnityEngine;

namespace Utils
{
    public class AddHealth : MonoBehaviour
    {
        public float healValue;
        private PlayerHealth _playerHealth;
        
        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerHealth.Heal(healValue);
                Destroy(gameObject);
            }
        }
    }
}
