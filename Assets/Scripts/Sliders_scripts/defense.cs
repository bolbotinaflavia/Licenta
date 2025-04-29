using Battle;
using Player;

namespace Sliders_scripts
{
    public class Defense:MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            if (BattleSystem.Instance.State == BattleState.PlayerAction)
            {
                StartCoroutine(BattleSystem.Instance.PlayerActionMove("defense"));
            }
            else
            {
                StartCoroutine(PlayerManager.Instance.notification_show("It's not your turn!"));
            }
            menuOption.value = 1;
        }
    }
}