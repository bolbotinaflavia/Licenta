namespace Sliders_scripts
{
    public class Run:MenuCountdown
    {
        protected override void OnTimerComplete()
        { 
           GameController.Instance.StopBattle();
           this.menuOption.value = 1;
        }
    }
}