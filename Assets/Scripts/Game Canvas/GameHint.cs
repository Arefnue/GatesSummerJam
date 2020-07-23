using UnityEngine;

namespace Game_Canvas
{
    public class GameHint : MonoBehaviour
    {
        [SerializeField] private GameObject gameHintGo;
        
        public void ShowGameHint() {
            if (gameHintGo.activeSelf)
            {
                HideGameHint();
                return;
            }
            Time.timeScale = 0;
            gameHintGo.SetActive(true);
        }

        public void HideGameHint()
        {
            gameHintGo.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
