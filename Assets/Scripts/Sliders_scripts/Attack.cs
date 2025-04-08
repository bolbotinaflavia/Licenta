using Battle;

namespace Sliders_scripts
{
    public class Attack:MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            if (BattleSystem.Instance.State == BattleState.PlayerAction)
            {
                BattleSystem.Instance.HandleActionSelector("attack");
            }
        }
    }
}