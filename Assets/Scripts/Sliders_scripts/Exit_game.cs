using Player;
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
                Destroy(PlayerManager.Instance.gameObject);
<<<<<<< HEAD
                MenuManager.Instance.LoadPrevious();
=======
                MenuManager.Instance.BackToPrevious();
>>>>>>> origin/fight_Scene
                Destroy(VolumeManager.Instance.gameObject);
            }
            menuOption.value = 1;
              StartCoroutine(Deselect());
        }

        // Start is called before the first frame update
    }
}
