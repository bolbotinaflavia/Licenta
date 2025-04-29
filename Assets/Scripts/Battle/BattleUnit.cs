using Enemies;
using Inventory;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [FormerlySerializedAs("_enemy_base")] [SerializeField] private EnemieBase enemyBase;

        [FormerlySerializedAs("HP_enemy_a")] [SerializeField]
        private HpBarAnimation hpEnemyA;
         
        public EnemieBase EnemieBase { get; set; }

      
        [SerializeField] private float hp;

        public float Hp
        {
            get => hp;
            private set => hp=value;
        }

        // public BattleUnit(EnemieBase enemieBase, int hp)
        // {
        //     this._enemy_base=enemieBase;
        //     this.Hp=hp;
        // }
        
        public void Setup_Enemy(string enemyName)
        {
           //this._enemy_base=EnemieBase.Instantiate(_enemy_base);
           this.enemyBase=Resources.Load<EnemieBase>($"Enemies/{enemyName}");
           //this._enemy_base = Instantiate(_enemy_base);
           this.GetComponent<SpriteRenderer>().sprite = enemyBase.Sprite1;
            this.hp = enemyBase.HpMax;
            Debug.Log(" Enemy is: "+this.enemyBase.name+" with hp: " + this.hp);
        }

        public int Attack(Moves move,PlayerManager player)
        {
            Debug.Log("the move is: " + move.MoveName);
            float modifiers = Random.Range(0f+move.Accuracy*0.01f, 1f);
            float d;
            d = (3f*move.Power+2f*enemyBase.Attack)/10f;
            d = d*modifiers-player.defense/10;
            Debug.Log("Player damage is: "+d);
            return d < 0 ? 0 : Mathf.FloorToInt(d);
        }

        public void AttackedBySpell(Spells.Spell spell, PlayerManager player)
        {
            float modifiers = Random.Range(0f+spell.SpellBase.Accuracy*0.01f, 1f);
            float d;
            d = (3f * spell.SpellBase.Power + 2f*player.attackSpeed) / 10f;
            d= d*modifiers-enemyBase.Defense/10;
            this.hp = this.hp - d;
            Debug.Log("Enemy damage is: "+d);
            hpEnemyA.damaging_animation();
            
        }
        public void Attacked(WeaponB w, PlayerManager player)
        {
            Debug.Log("Attacked enemy");
            float modifiers = Random.Range(0.5f, 1f);
            float attackWeapon = w.Damage;
            float d;
            if (enemyBase.W1.ToString().Equals(w.WeaponName))
                d = (3f * attackWeapon + 2f*player.attackSpeed) / 10f;
            else
                d = (4f * attackWeapon + 2f*player.attackSpeed) / 10f;
            d = d*modifiers-enemyBase.Defense/10;
            this.hp = this.hp - d;
            Debug.Log("Enemy damage is: "+d);
            hpEnemyA.damaging_animation();
        }

        public Moves getRandomMove()
        {
            int r=Random.Range(0, enemyBase.Moves.Count);
            return enemyBase.Moves[r].Move;
        }    }
}