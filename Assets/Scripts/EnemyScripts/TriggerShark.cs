using System;
using UnityEngine;

namespace EnemyScripts
{
    public class TriggerShark : MonoBehaviour
    {
        public SharkController controller;
        private void Start()
        {
            transform.parent = null;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !controller.attackTriggered)
            {
                controller.attackTriggered = true;
                controller.sharkState = SharkController.SharkState.Attack;
                controller.DetermineState();
            }
        }
    }
}
