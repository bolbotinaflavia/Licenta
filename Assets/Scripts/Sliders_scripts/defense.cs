using Battle;
using UnityEngine;

namespace Sliders_scripts
{
    public class defense:Menu_countdown
    {
        protected override void OnTimerComplete()
        {
            if (BattleSystem.instance.State == BattleState.PlayerAction)
            {
                BattleSystem.instance.HandleActionSelector("defense");
            }
        }
    }
}