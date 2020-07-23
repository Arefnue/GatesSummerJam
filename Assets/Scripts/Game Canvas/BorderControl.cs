using System;
using PlayerScripts;
using UnityEngine;

namespace Game_Canvas
{
    public class BorderControl : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerHealth.TakeDamage(200);
            }
        }
    }
}
