namespace Sliders_scripts
{
    public class Run:MenuCountdown
    {
        protected override void OnTimerComplete()
        { 
           GameController.Instance.StopBattle();
        }
    }
}