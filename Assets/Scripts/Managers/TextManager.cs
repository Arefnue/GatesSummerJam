using TMPro;
using UnityEngine;

namespace Managers
{
    public class TextManager : MonoBehaviour
    {
        #region Singleton

        public static TextManager Manager;


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



        public TextMeshProUGUI paperText;
        public GameObject paperPanel;


        public void OpenPaperPanel(string text)
        {
            Time.timeScale = 0f;
            paperText.text = text;
            paperPanel.SetActive(true);
        }

        public void ClosePaperPanel()
        {
        
            paperPanel.SetActive(false);
            paperText.text = "";
            Time.timeScale = 1f;
        }
    
    }
}
