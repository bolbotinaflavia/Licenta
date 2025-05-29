using Player;
using UnityEngine.SceneManagement;

namespace Sliders_scripts
{
    public class Restart_game:MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            if (MenuManager.Instance != null)
            {
                SceneManager.LoadScene("StartGame");
                MenuManager.Instance.BackToPrevious();
            }
            menuOption.value = 1;
        }
    }
}