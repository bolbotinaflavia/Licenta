using UnityEngine.SceneManagement;

namespace Sliders_scripts
{
    public class ExitFight : MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            SceneManager.LoadScene("Gameplay");
            menuOption.value = 1;
        }

        // Start is called before the first frame update
    }
}
