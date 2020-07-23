using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;


namespace Game_Canvas
{
    public class LoseController : MonoBehaviour
    {
        private GameObject losePopup;
        private PlayerScripts.PlayerHealth _playerHealth;
        private LevelController levelController;
        private bool isLosePopupDisplayed = false;


        [TextArea]
        public List<string> textList;

        public TextMeshProUGUI loseText;
        
        
        void Start() {
            _playerHealth = FindObjectOfType<PlayerScripts.PlayerHealth>();
            losePopup = transform.GetChild(0).gameObject;
            levelController = FindObjectOfType<LevelController>();

            GameManager.Manager.onGameOver += CheckLose;

            if(!levelController || !losePopup || !_playerHealth)
            {
                throw new System.Exception("Scene does not contain expected Object");
            }
        }
        

        public void CheckLose()
        {
            if (!isLosePopupDisplayed)
            {
                ShowLosePopup();
            }
        }

        private void ShowLosePopup()
        {
            var randomIndex = Random.Range(0, textList.Count);
            GameManager.Manager.gameOver = true;
            loseText.text = textList[randomIndex];
            isLosePopupDisplayed = true;
            losePopup.SetActive(true);

            Time.timeScale = 0;
        }

        public void RestartGame()
        {
            levelController.RestartScene();
        }
    }
}