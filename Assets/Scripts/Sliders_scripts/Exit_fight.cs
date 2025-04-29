using UnityEngine.SceneManagement;

namespace Sliders_scripts
{
    public class ExitFight : MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            SceneManager.LoadScene("Gameplay");
        }

        // Start is called before the first frame update
    }
}
