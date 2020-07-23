using UnityEngine;

namespace PlayerScripts
{
    public class TriggerTrap : MonoBehaviour
    {
        public TrapController controller;

        private bool _isTriggered;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_isTriggered)
            {
                _isTriggered = true;
                Invoke(nameof(Refresh),1f);
                controller.TriggerTrap();
            }
        }
    
        private void Refresh()
        {
            _isTriggered = false;
        }
    }
}
