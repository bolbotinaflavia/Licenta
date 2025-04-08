using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Enemies/Create new enemy")]
    public class EnemieBase : ScriptableObject
    {
        [FormerlySerializedAs("name")] [SerializeField] private string enemyName;

        [TextArea]
        [SerializeField] private string description;

        [SerializeField] private Sprite sprite1;
        //aici ar trebui animatii
        [FormerlySerializedAs("attack_s")] [SerializeField] private Sprite attackS;
        [FormerlySerializedAs("defense_s")] [SerializeField] private Sprite defenseS;
        [SerializeField] private WeaponWeakness w1;
        [SerializeField] private SpellWeakness w2;
    
        //Base stats
    
        [FormerlySerializedAs("hp_max")] [SerializeField] private float hpMax;
        [SerializeField] private int attack;
        [SerializeField] private float defense;
        [SerializeField] private int speed;

        public List<string> get_all_enemies_entities()
        {
            var allEnemies = GameObject.FindObjectsOfType<EnemieBase>().ToList().ConvertAll(x => x.enemyName);
            return allEnemies;
        }

        public float HpMax { get => hpMax; set => hpMax = value; }

        public int Attack => attack;

        public float Defense => defense;

        public int Speed => speed;
        public WeaponWeakness W1 { get => w1; set => w1 = value; }
        public SpellWeakness W2 { get => w2; set => w2 = value; }
    }

    public enum WeaponWeakness
    {
        //all specials are +5 damage depending on the type
        Sword, //general use
        Crossbow, //good for flighting enemies(ghosts)
        Trowel, //close contact enemies(zombies)
        Shovel // good for vampires, skeletons
    
    
    }

    public enum SpellWeakness
    {
//names 
        Brisingr,//fire spell
        Jierda,//cracking bones
        Slytha, //sleep spell
        HolyBlaze,//blinding light
    
    }
}