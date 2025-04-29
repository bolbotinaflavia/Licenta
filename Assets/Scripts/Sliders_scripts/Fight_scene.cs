using UnityEngine;

namespace Sliders_scripts
{
    public class FightScene : MenuCountdown
    {
        public int actiune;

        protected override void OnTimerComplete()
        {
            //attack
            if (actiune == 1)
                Debug.Log("Attack");
            //defense
            if (actiune == 2)
                Debug.Log("Defense");
            //spell
            if (actiune == 3)
                Debug.Log("Spell");

        }

    }
}
