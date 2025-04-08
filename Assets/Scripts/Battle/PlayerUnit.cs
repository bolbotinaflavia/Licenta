using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle
{
    public class PlayerUnit:MonoBehaviour
    {
        [FormerlySerializedAs("_playerUnit")] [SerializeField] private PlayerManager playerUnit;
        [SerializeField] private float hp;
        [SerializeField] public int defense;
        [SerializeField] public int attackSpeed;
        [FormerlySerializedAs("HP_anim")] [SerializeField] private HpBarAnimation hpAnim;
        
        public float Hp
        {
            get => hp;
            set => hp = value;
        }

        public int Defense
        {
            get => defense;
            set => defense = value;
        }

        public int AttackSpeed
        {
            get => attackSpeed;
            set => attackSpeed=value;
        }
        public void Setup()
        {
            playerUnit = GetComponent<PlayerManager>();
        }

        public void current_weapon()
        {
               
        }

        // public int attack()
        // {
        //    
        // }
        public void defense_battle(int enemyAttack)
        {
            Hp -= enemyAttack + (int)(defense * 0.1);
            hpAnim.damaging_animation();
        }
    }
}