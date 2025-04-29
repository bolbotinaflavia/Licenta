using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Sliders_scripts
{
    public class ExitGame : MenuCountdown
    {
        [FormerlySerializedAs("next_menu")] public string nextMenu;

        protected override void OnTimerComplete()
        {
            if (MenuManager.Instance != null)
            {
                SceneManager.LoadScene("StartGame");
                MenuManager.Instance.BackToPrevious();
            }
            menuOption.value = 1;
        }

        // Start is called before the first frame update
    }
}
