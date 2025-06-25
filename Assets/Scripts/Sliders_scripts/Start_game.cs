using Inventory;
using Player;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Sliders_scripts
{
    public class StartGame: MenuCountdown
    {
        [FormerlySerializedAs("next_menu")] public string nextMenu;
        //public AudioSource basic;

        protected override void OnTimerComplete()
        {
            menuOption.value = 1;
            MenuManager.Instance.LoadMenu(nextMenu);
            MenuManager.Instance.gameStarted = true;
            MenuManager.Instance.currentMenu.SetActive(false);
            SceneManager.LoadScene("StoryStart");
            menuOption.value = 1;
            //StartCoroutine(Deselect());
        }
        // Start is called before the first frame update
       
    }
}
