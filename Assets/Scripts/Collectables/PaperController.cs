using Managers;
using UnityEngine;

namespace Collectables
{
    public class PaperController : MonoBehaviour
    {

        [TextArea]
        public string paperText;

        private bool isPicked = false; // When player lands on paper, function triggered 2 times
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !isPicked)
            {
                isPicked = true;
                TextManager.Manager.OpenPaperPanel(paperText);
                GameManager.Manager.collectedPaperCount++;
                Destroy(gameObject);
            }
        }
    }
}
