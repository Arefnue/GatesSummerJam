using System;
using UnityEditor;
using UnityEngine;

namespace EnemyScripts
{
    public class FrogBossTrigger : MonoBehaviour
    {
        public FrogBoss boss;
        public bool isStart;

        private void Start()
        {
            boss.onDeath += OnDeath;
        }


        public void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (isStart)
                {
                    boss.WakeFrog();
                }
                else
                {
                    boss.SleepFrog();
                }
            }
        }
    }
}
