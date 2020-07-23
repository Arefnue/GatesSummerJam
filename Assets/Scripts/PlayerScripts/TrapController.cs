using System;
using Managers;
using UnityEngine;

namespace PlayerScripts
{
    public class TrapController : MonoBehaviour
    {

        public Animator anim;
        public float damage;
        private PlayerHealth _playerHealth;
        private static readonly int Trigger = Animator.StringToHash("Trigger");
        public AudioClip vomitClip;

        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }


        public void TriggerTrap()
        {
            Debug.Log("Trap triggered!");
            anim.SetTrigger(Trigger);
            _playerHealth.TakeDamage(damage);
            AudioManager.Manager.PlaySfx(vomitClip);
        }
    
    }
}
