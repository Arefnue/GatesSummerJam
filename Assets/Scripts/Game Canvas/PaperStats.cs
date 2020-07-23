using TMPro;
using UnityEngine;

namespace Game_Canvas
{
    public class PaperStats : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private Managers.GameManager gameManager;
        private int totalPaperCount = 0;


        void Start()
        {
            text = transform.GetComponentInChildren<TextMeshProUGUI>();
            gameManager = FindObjectOfType<Managers.GameManager>();
            totalPaperCount = FindObjectsOfType<Collectables.PaperController>().Length;
        }

        void Update()
        {
            text.text = gameManager.collectedPaperCount.ToString() + " / " + totalPaperCount;
        }
    }
}
