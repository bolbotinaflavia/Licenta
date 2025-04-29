namespace Sliders_scripts
{
    public class OpenPlayerMenu :MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            menuOption.value = 1;
            MenuManager.Instance.currentMenu.SetActive(MenuManager.Instance.currentMenu.activeSelf != true);
        }

        // Start is called before the first frame update
    
    }
}
