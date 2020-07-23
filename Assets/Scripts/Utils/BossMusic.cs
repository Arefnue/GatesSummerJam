using System;
using Managers;
using UnityEngine;

namespace Utils
{
    public class BossMusic : MonoBehaviour
    {
        public AudioClip bossMusic;
        public AudioClip defaultMusic;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                AudioManager.Manager.PlayMusic(bossMusic);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                AudioManager.Manager.PlayMusic(defaultMusic);
            }
        }
    }
}
