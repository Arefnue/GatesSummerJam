using Managers;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Game_Canvas
{
    public class HealthIndicator : MonoBehaviour
    {
        // Cached references
        private Slider _healthSlider;
        private PlayerHealth _playerHealth;
        private bool _gameOver;

        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _healthSlider = GetComponent<Slider>();

            _healthSlider.maxValue = _playerHealth.health; // player's health on initial load is used as maximum value
            _healthSlider.value = _playerHealth.health;
            _healthSlider.minValue = 0;
        }

        private void Update()
        {
            if (_gameOver)
            {
                return;
            }
            //_healthSlider.value = _playerHealth.health; // Can be improved using Actor Model, Send message. i.e. triggering function like PlayerHealth
            _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, _playerHealth.health, Time.deltaTime*100);
            if (_healthSlider.value <= 0)
            {
                _gameOver = true;
                GameManager.Manager.GameOver();
            }
            
        }
    }
}

