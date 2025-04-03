using Battle;
using UnityEngine.SceneManagement;

namespace Sliders_scripts
{
    public class attack:Menu_countdown
    {
        protected override void OnTimerComplete()
        {
            if (BattleSystem.instance.State == BattleState.PlayerAction)
            {
                BattleSystem.instance.HandleActionSelector("attack");
            }
        }
    }
}