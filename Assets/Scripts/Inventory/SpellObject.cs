using System.Collections.Generic;
using Player;
using Spells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class Spell : MonoBehaviour
    {
        //[SerializeField] private SpellBase _spellBase;
        [FormerlySerializedAs("spell_name")] public string spellName;
        [SerializeField] private SpellLevel magicLevel;
        [SerializeField] private string description2; //more information about the spell, what does=>Initiate
        [SerializeField] private string description3;
        [SerializeField] private int power;
        [SerializeField] private int accuracy;
        public int Power
        {
            get => power;
        }

        public int Accuracy
        {
            get => accuracy;
        }

        public string Description2
        {
            get => description2;
        }

        public string Description3
        {
            get => description3;
        }
    
        // Start is called before the first frame update
        public void level_up(PlayerManager player)
        {
            magicLevel++;
        }

        public int get_magic_level()
        {
            return (int)magicLevel;
        }
    
        public void learning_spell()
        {
            Debug.Log("Animation started");
            //de facut animatie
        
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
                description.Add(name);
                Debug.Log(name);
                description.Add("No information yet");
                return description;
            }

            if (magicLevel.Equals(2))
            {
                description.Add(name);
                Debug.Log(name);
                description.Add(description2);
                Debug.Log(description2);
                return description;
            }

            if (magicLevel.Equals(3))
            {
                description.Add(name);
                Debug.Log(name);
                description.Add(description2);
                Debug.Log(description2);
                description.Add(description3);
                Debug.Log(description3);
                return description;
            }

            return description;

        }
        // Update is called once per frame
    }
}