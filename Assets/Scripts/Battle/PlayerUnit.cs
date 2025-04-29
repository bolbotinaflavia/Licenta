using Inventory;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Battle
{
    public class PlayerUnit:MonoBehaviour
    {
        [FormerlySerializedAs("_playerUnit")] [SerializeField] public  PlayerManager player;
        [SerializeField] private float hp;
        [SerializeField] public int defense;
        [SerializeField] public int attack;
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
            get => attack;
            set => attack=value;
        }

        public HpBarAnimation HpAnim
        {
            get => hpAnim;
            set => hpAnim = value;
        }
        public void Setup()
        {
            player = GetComponent<PlayerManager>();
            attack = player.attackSpeed;
            defense = player.defense;
        }

        public void current_weapon()
        {
               
        }

        // public int attack()
        // {
        //    
        // }
        public WeaponB AttackBattle()
        {
            return InventoryManager.Instance.Attack();
        }
        public void DefenseBattle(int enemyAttack)
        {
            player.hp -= enemyAttack + (int)(defense * 0.1);
            hpAnim.damaging_animation();
        }
    }
}