using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] private EnemieBase _enemy_base;
            [SerializeField] Hp_Bar_Animation HP_enemy_a;
        public EnemieBase _Enemie_Base { get; set; }
        [SerializeField] private float hp;

        public float Hp
        {
            get { return hp; }
            set{hp=value;}
        }

        // public BattleUnit(EnemieBase enemieBase, int hp)
        // {
        //     this._enemy_base=enemieBase;
        //     this.Hp=hp;
        // }
        
        public void Setup_Enemy()
        {
           //this._enemy_base=EnemieBase.Instantiate(_enemy_base);
           this._enemy_base=Resources.Load<EnemieBase>($"Enemies/{_enemy_base.name}");
           //this._enemy_base = Instantiate(_enemy_base);
            this.hp = _enemy_base.Hp_max;
            Debug.Log(" Enemy is: "+this._enemy_base.name+" with hp: " + this.hp);
        }

        public int attack()
        {
            return _enemy_base.Attack;
        }

        public void Attacked(Weapons w)
        {
            Debug.Log("Attacked enemy");
            float attackDamage = w.damage;
            if (_enemy_base.W1.ToString().Equals(w.name))
                this.hp = this.hp - attackDamage * 0.5f;
            else
                this.Hp = this.Hp -attackDamage + _enemy_base.Defense * 0.1f;
            Debug.Log("enemy hp is: "+this.Hp+" after attack");
            HP_enemy_a.damaging_animation();
        }
        
    }
}