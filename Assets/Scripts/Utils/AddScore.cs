using System;
using Managers;
using UnityEngine;

namespace Utils
{
    public class AddScore : MonoBehaviour
    {
        public AudioClip scoreClip;
        private bool _checked;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_checked)
            {
                _checked = true;
                AudioManager.Manager.PlaySfx(scoreClip);
                GameManager.Manager.collectedSoulCount += 1;
                Destroy(gameObject);
            }
        }
    }
}
