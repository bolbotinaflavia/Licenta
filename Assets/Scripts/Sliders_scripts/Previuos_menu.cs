using System.Dynamic;
using UnityEditor;

namespace Sliders_scripts
{
    public class PreviuosMenu : MenuCountdown
    {
        public bool isFight;
        protected override void OnTimerComplete()
        {
            menuOption.value = 1;
            if (GameController.Instance != null)
            {
                if (GameController.Instance.state == GameState.Battle)
                {
                    MenuManager.Instance.battlePreviousMenu();
                }
                else
                {
                    MenuManager.Instance.BackToPrevious();
                }
            }
            else
            {
                MenuManager.Instance.BackToPrevious();
            }

        }

        // Start is called before the first frame update
       
    }
}
