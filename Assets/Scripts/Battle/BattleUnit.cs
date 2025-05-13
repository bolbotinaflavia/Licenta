using Enemies;
using Inventory;
using Player;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [FormerlySerializedAs("_enemy_base")] [SerializeField]
        private EnemieBase enemyBase;

        [FormerlySerializedAs("HP_enemy_a")] [SerializeField]
        private HpBarAnimation hpEnemyA;

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

        private bool isAttacking;

        public bool IsAttacking
        {
            get => isAttacking;
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

        private AnimatorOverrideController overrideController;
        public EnemieBase EnemieBase { get; set; }


        [SerializeField] private float hp;

        public float Hp
        {
            get => hp;
            private set => hp = value;
        }

        // public BattleUnit(EnemieBase enemieBase, int hp)
        // {
        //     this._enemy_base=enemieBase;
        //     this.Hp=hp;
        // }

        public void Setup_Enemy(string enemyName)
        {
            //this._enemy_base=EnemieBase.Instantiate(_enemy_base);
            this.enemyBase = Resources.Load<EnemieBase>($"Enemies/{enemyName}");
            //this._enemy_base = Instantiate(_enemy_base);
            this.GetComponent<SpriteRenderer>().sprite = enemyBase.Sprite1;
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = enemyBase.Animator;

            this.hp = enemyBase.HpMax;
            Debug.Log(" Enemy is: " + this.enemyBase.name + " with hp: " + this.hp);
        }

        public void Attack(Moves move, PlayerManager player)
        {
            Debug.Log("the move is: " + move.MoveName);
            float modifiers = Random.Range(0f + move.Accuracy * 0.01f, 1f);
            float d;
            d = (3f * move.Power + 2f * enemyBase.Attack) / 10f;
            d = d * modifiers - player.defense / 10;
            Debug.Log("Player damage is: " + d);
            if (d > 0)
            {
                player.hp -= d;
                Debug.Log("player health is: "+player.hp);
            }
        }

        public void AttackedBySpell(Spells.Spell spell, PlayerManager player)
        {
            float modifiers = Random.Range(0f + spell.SpellBase.Accuracy * 0.01f, 1f);
            float d;
            d = (3f * spell.SpellBase.Power + 2f * player.attackSpeed) / 10f;
            d = d * modifiers - enemyBase.Defense / 10;
            this.hp = this.hp - d;
            Debug.Log("Enemy damage is: " + d);

        }

        public void Attacked(WeaponB w, PlayerManager player)
        {
            Debug.Log("Attacked enemy");
            float modifiers = Random.Range(0.5f, 1f);
            float attackWeapon = w.Damage;
            float d;
            if (enemyBase.W1.ToString().Equals(w.WeaponName))
                d = (3f * attackWeapon + 2f * player.attackSpeed) / 10f;
            else
                d = (4f * attackWeapon + 2f * player.attackSpeed) / 10f;
            d = d * modifiers - enemyBase.Defense / 10;
            this.hp = this.hp - d;
            Debug.Log("Enemy damage is: " + d);
        }

        public Moves getRandomMove()
        {
            int r = Random.Range(0, enemyBase.Moves.Count);
            return enemyBase.Moves[r].Move;
        }
    }
}