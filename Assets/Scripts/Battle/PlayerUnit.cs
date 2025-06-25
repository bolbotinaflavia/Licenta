using Animations;
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
        public Animator animator { get; set; }

        public bool IsIdle
        {
            get => IsIdle;
            set
            {
                if (animator != null)
                {
                    animator.SetBool("isIdle", value);
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
            }
        }

        public bool IsAttacking
        {
            get => IsAttacking;
            set
            {
                if (animator != null)
                {
                    animator.SetBool("isAttacking", value);
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
            }
        }

        public bool IsAttacked
        {
            get => IsAttacked;
            set
            {
                if (animator != null)
                {
                    animator.SetBool("isAttacked", value);
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
            }
        }
        
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
            player = PlayerManager.Instance;
            animator = GetComponent<Animator>();
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