using UnityEngine.Serialization;

namespace Sliders_scripts
{
    public class NextMenu : MenuCountdown
    {
        [FormerlySerializedAs("next_menu")] public string nextMenu;
        protected override void OnTimerComplete()
        {
            menuOption.value = 1;
            MenuManager.Instance.LoadMenu(nextMenu);
            menuOption.value = 1;
              StartCoroutine(Deselect());
        }

        // Start is called before the first frame update
    }
}
