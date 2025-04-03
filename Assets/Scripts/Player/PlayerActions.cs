using Spells;
using UnityEngine;

namespace Player
{
    public class PlayerActions:PlayerManager
    {
        public PlayerActions Instance;
        //spells
        // public void learn_spell(Spell spell)
        // {
        //     if (spell != null)
        //     {
        //     
        //         spell.discover();
        //         spells.Add(spell);
        //         Debug.Log("Learned spell: " + spell.name);
        //     }
        // }
        //spell base
        public void learn_spell(Spell s)
        {
            if (s != null)
            {
                this._spells.Add(s);
                s.level_up(this);
            }
        }
    }
}