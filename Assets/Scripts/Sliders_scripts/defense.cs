using Battle;

namespace Sliders_scripts
{
    public class Defense:MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            if (BattleSystem.Instance.State == BattleState.PlayerAction)
            {
                BattleSystem.Instance.HandleActionSelector("defense");
            }
        }
    }
}