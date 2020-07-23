using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game_Canvas
{
    public class WinController : MonoBehaviour
    {
        private GameObject winPopup;
        private Managers.GameManager gameManager;
        private LevelController levelController;

        //private int totalPaperCount = 0;
        private int totalSpiritCount = 0;

        private bool isWinPopupDisplayed = false;

        [TextArea]
        public List<string> textList;

        public TextMeshProUGUI winText;

        void Start()
        {
            gameManager = FindObjectOfType<Managers.GameManager>();
            winPopup = transform.GetChild(0).gameObject;
            levelController = FindObjectOfType<LevelController>();

            totalSpiritCount = gameManager.GetTotalSoulCount();


            if (!gameManager || !winPopup || !levelController)
            {
                throw new System.Exception("Scene does not contain expected Object");
            }
        }

        void Update()
        {
            if (!isWinPopupDisplayed && totalSpiritCount <= gameManager.collectedSoulCount)
            {
                var randomIndex = Random.Range(0, textList.Count);
                winText.text = textList[randomIndex];
                ShowWinPopup();
            }
        }

        private void ShowWinPopup()
        {
            isWinPopupDisplayed = true;
            GameManager.Manager.gameOver = true;
            winPopup.SetActive(true);

            Time.timeScale = 0;
        }

        public void RestartGame()
        {
            levelController.RestartScene();
        }
    }
}