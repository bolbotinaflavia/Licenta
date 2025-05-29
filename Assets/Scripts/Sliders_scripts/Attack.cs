using Battle;
using Player;

namespace Sliders_scripts
{
    public class Attack:MenuCountdown
    {
        protected override void OnTimerComplete()
        {
            if (BattleSystem.Instance.State == BattleState.PlayerAction)
            {
                
               StartCoroutine( BattleSystem.Instance.PlayerActionMove("attack"));
               
            }
            else
            {
                StartCoroutine(BattleSystem.Instance.Notification.notification_show("It's not your turn!",2f));
            }
            menuOption.value = 1;
        }
    }
}