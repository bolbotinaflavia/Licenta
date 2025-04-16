using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Spells
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private SpellBase spellBase;
        [SerializeField] private SpellLevel magicLevel;

        public SpellBase SpellBase
        {
            get => spellBase;
            set => spellBase = value;
        }
        
        public void level_up()
        {
            magicLevel++;
        }

        public int get_magic_level()
        {
            return (int)magicLevel;
        }

        public List<string> get_description()
        {
            List<string> description = new List<string>();
            if (magicLevel.Equals(0))
            {
                description.Add("No information yet");
                return description;
            }

            if (magicLevel.Equals(1))
            {
                //escription.Add(spellBase.name);
                //Debug.Log(name);
                description.Add("No information yet");
                return description;
            }

            if (magicLevel.Equals(2))
            {
                // description.Add(name);
                description.Add(spellBase.Description2);
                return description;
            }

            if (magicLevel.Equals(3))
            {
                //  description.Add(name);
                //Debug.Log(name);
                description.Add(spellBase.Description2);
                //Debug.Log(description2);
                description.Add(spellBase.Description3);
                //Debug.Log(description3);
                return description;
            }

            return description;
        }
        public void learning_spell()
        {
            Debug.Log("Animation started");
            //de facut animatie

        }
    }
}