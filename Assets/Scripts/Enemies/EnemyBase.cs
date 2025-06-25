using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Enemies/Create new enemy")]
    public class EnemieBase : ScriptableObject
    {
        [FormerlySerializedAs("name")][SerializeField] private string enemyName;

        [TextArea]
        [SerializeField] private string description;

        [SerializeField] private Sprite sprite1;
        [SerializeField] private Type type;
        public Sprite Sprite1 => sprite1;
        //aici ar trebui animatii
        [FormerlySerializedAs("_animator")][SerializeField] private RuntimeAnimatorController animator;

        [SerializeField] private WeaponWeakness w1;
        [SerializeField] private SpellWeakness w2;

        //Base stats

        [FormerlySerializedAs("hp_max")][SerializeField] private float hpMax;
        [SerializeField] private int attack;
        [SerializeField] private float defense;
        [SerializeField] private int speed;
        [SerializeField] private List<LearnableMoves> moves;

        public RuntimeAnimatorController Animator => animator;
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
        public List<LearnableMoves> Moves { get => moves; set => moves = value; }
        
        public EnemieBase(string name, string description, Sprite sprite1, Type type, RuntimeAnimatorController animator, WeaponWeakness w1, SpellWeakness w2, float hpMax, int attack, float defense, int speed, List<LearnableMoves> moves)
        {
            this.enemyName = name;
            this.description = description;
            this.sprite1 = sprite1;
            this.type = type;
            this.animator = animator;
            this.w1 = w1;
            this.w2 = w2;
            this.hpMax = hpMax;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
            this.moves = moves;
        }
      
    }
[System.Serializable]
    public class LearnableMoves
    {
        [SerializeField] private Moves move;
        public Moves Move { get => move; set => move = value; }
    }

    public enum Type
    {
        Basic,
        Hard,
        Magician
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