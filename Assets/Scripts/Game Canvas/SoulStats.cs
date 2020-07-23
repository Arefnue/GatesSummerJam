using TMPro;
using UnityEngine;

namespace Game_Canvas
{
    public class SoulStats : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private Managers.GameManager gameManager;
        private int totalSoulCount = 0;


        void Start()
        {
            text = transform.GetComponentInChildren<TextMeshProUGUI>();
            gameManager = FindObjectOfType<Managers.GameManager>();
            totalSoulCount = gameManager.GetTotalSoulCount();


            if (!gameManager || !text)
            {
                throw new System.Exception("Scene does not contain expected Object");
            }
        }

        void Update()
        {
            text.text = gameManager.collectedSoulCount.ToString() + " / " + totalSoulCount;
        }
    }
}
