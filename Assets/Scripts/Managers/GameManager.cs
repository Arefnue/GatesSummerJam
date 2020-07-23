using System;
using EnemyScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Manager;


        private void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        #endregion

    
        public Action onGameOver;

        public int collectedPaperCount = 0;
        public int collectedSoulCount = 0;

        public bool gameOver;
        
        
        public void GameOver()
        {
            onGameOver?.Invoke();
        }

        public int GetTotalSoulCount()
        {
            var score = GameObject.FindGameObjectsWithTag("Score").Length; // todo if spirits are on ground add to count
            score += FindObjectsOfType<FrogBoss>().Length;
            score += FindObjectsOfType<BoruAdamController>().Length;

            return score;
        }

    }
}
