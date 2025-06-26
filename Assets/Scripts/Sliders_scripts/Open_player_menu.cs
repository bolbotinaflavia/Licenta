using Player;

namespace Sliders_scripts
{
    public class OpenPlayerMenu :MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            menuOption.value = 1;
            MenuManager.Instance.currentMenu.SetActive(MenuManager.Instance.currentMenu.activeSelf != true);
            if (MenuManager.Instance.currentMenu.activeSelf)
            {
                GameController.Instance.state = GameState.Menu;
                if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove"))
                {
                    PlayerMovement.Instance.CurrentControl.load_sliders();
                }
              
            }
            else
            {
                GameController.Instance.state = GameState.FreeRoam;
            }
             StartCoroutine(Deselect());
        }

        // Start is called before the first frame update
    
    }
}
