using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Spells/ New Spell")]
    public class SpellBase : ScriptableObject
    {
        [SerializeField] private string name; // basic, found just the name of the spell=>Apprentice
        [SerializeField] private int power;
        [SerializeField] private int accuracy;
        [SerializeField] private string description2; //more information about the spell, what does=>Initiate
        [SerializeField] private string description3; //more information, about the enemies it affects=> Disciple

        

        // Start is called before the first frame update
        

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
        

        
    }

    public enum SpellLevel
    {
        none,
        Apprentice, //
        Initiate,
        Disciple

    }
}