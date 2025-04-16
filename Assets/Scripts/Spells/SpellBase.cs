using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spells
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Spells/ New Spell")]
    public class SpellBase : ScriptableObject
    {
        [FormerlySerializedAs("name")] [SerializeField]
        private string spellName; // basic, found just the name of the spell=>Apprentice

        [SerializeField] private int power;
        [SerializeField] private int accuracy;
        [SerializeField] private string description2; //more information about the spell, what does=>Initiate
        [SerializeField] private string description3; //more information, about the enemies it affects=> Disciple


        // Start is called before the first frame update


        public string SpellName { get => spellName; set => spellName = value; }
        public int Power => power;

        public int Accuracy => accuracy;

        public string Description2 => description2;

        public string Description3 => description3;
    }

    public enum SpellLevel
    {
        None,
        Apprentice, //
        Initiate,
        Disciple

    }
}