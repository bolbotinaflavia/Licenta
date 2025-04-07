using Unity.VisualScripting;
using UnityEngine;

namespace Battle
{
    public class PlayerUnit:MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerUnit;
        [SerializeField] float hp;
        [SerializeField] public int defense;
        [SerializeField] public int attackSpeed;
        [SerializeField] Hp_Bar_Animation HP_anim;
        
        public float Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        public int AttackSpeed
        {
            get { return attackSpeed;}
            set{attackSpeed=value;}
        }
        public void Setup()
        {
            _playerUnit = GetComponent<PlayerManager>();
        }

        public void current_weapon()
        {
               
        }

        // public int attack()
        // {
        //    
        // }
        public void defense_battle(int enemy_attack)
        {
            Hp -= enemy_attack + (int)(defense * 0.1);
            HP_anim.damaging_animation();
        }
    }
}