using Enemies;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [FormerlySerializedAs("_enemy_base")] [SerializeField] private EnemieBase enemyBase;
            [FormerlySerializedAs("HP_enemy_a")] [SerializeField] private HpBarAnimation hpEnemyA;
        public EnemieBase EnemieBase { get; set; }
        [SerializeField] private float hp;

        public float Hp
        {
            get => hp;
            set => hp=value;
        }

        // public BattleUnit(EnemieBase enemieBase, int hp)
        // {
        //     this._enemy_base=enemieBase;
        //     this.Hp=hp;
        // }
        
        public void Setup_Enemy()
        {
           //this._enemy_base=EnemieBase.Instantiate(_enemy_base);
           this.enemyBase=Resources.Load<EnemieBase>($"Enemies/{enemyBase.name}");
           //this._enemy_base = Instantiate(_enemy_base);
            this.hp = enemyBase.HpMax;
            Debug.Log(" Enemy is: "+this.enemyBase.name+" with hp: " + this.hp);
        }

        public int Attack()
        {
            return enemyBase.Attack;
        }

        public void Attacked(WeaponBase w)
        {
            Debug.Log("Attacked enemy");
            float attackDamage = w.Damage;
            if (enemyBase.W1.ToString().Equals(w.WeaponName))
                this.hp = this.hp - attackDamage * 0.5f;
            else
                this.Hp = this.Hp -attackDamage + enemyBase.Defense * 0.1f;
            Debug.Log("enemy hp is: "+this.Hp+" after attack");
            hpEnemyA.damaging_animation();
        }
        
    }
}