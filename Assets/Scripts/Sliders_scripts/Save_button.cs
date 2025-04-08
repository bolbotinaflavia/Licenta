using UnityEngine;

namespace Sliders_scripts
{
    public class SaveButton : MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            //PlayerPrefs.SetFloat("Volume", Volume.Instance.s.volume);
            PlayerPrefs.Save();
            menuOption.value = 1;
            MenuManager.Instance.BackToPrevious();
        }

        // Start is called before the first frame update
     
    }
}
