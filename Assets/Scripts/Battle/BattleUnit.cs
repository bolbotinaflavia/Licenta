using Enemies;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Battle
{
    public class BattleUnit : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("isIdle");

        [FormerlySerializedAs("_enemy_base")] [SerializeField]
        private EnemieBase enemyBase;

        [FormerlySerializedAs("HP_enemy_a")] [SerializeField]
        private HpBarAnimation hpEnemyA;

        public Animator Animator { get; set; }

        public bool IsIdle
        {
            get => IsIdle;
            set
            {
                if (Animator != null)
                {
                    Animator.SetBool(Idle, value);
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
                if (Animator != null)
                {

                    Animator.SetBool("isAttacking", value);
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
                if (Animator != null)
                {
                    Animator.SetBool("isAttacked", value);
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
            Animator = GetComponent<Animator>();
            Animator.runtimeAnimatorController = enemyBase.Animator;

            this.hp = enemyBase.HpMax;
            Debug.Log(" Enemy is: " + this.enemyBase.name + " with hp: " + this.hp);
        }

        public void Attack(Moves move, PlayerManager player)
        {
            Debug.Log("the move is: " + move.MoveName);
            float modifiers = Random.Range(0f, 1f);
            float d;
            d = (enemyBase.Attack * 0.6f + move.Power * 0.4f) / 5f * (modifiers + 1);
            if (modifiers*move.Accuracy/10f > 1)
            {
                d = d - player.defense / 10f;
            }
            else
            {
                d = 0;
            }

            Debug.Log("Player damage is: " + d);
            if (d > 0)
            {
                player.hp -= d;
                Debug.Log("player health is: "+player.hp);
            }
        }

        public void AttackedBySpell(Spells.Spell spell, PlayerManager player)
        {
            float modifiers = Random.Range(0f, 1f);
            float d;
            float bonus = get_spell_bonus(spell, player);
            d = (spell.SpellBase.Power*0.4f +  player.attackSpeed*0.6f) / 4f*(1+bonus*2+modifiers);
            if (modifiers * spell.SpellBase.Accuracy / 10f > 1)
            {
                d = d - enemyBase.Defense / 10f;
            }
            else
            {
                d = 0;
            }
            this.hp = this.hp - d;
            Debug.Log("Enemy damage is after spell : " + d);

        }

        public void Attacked(WeaponB w, PlayerManager player)
        {
            Debug.Log("Attacked enemy");
            float modifiers = Random.Range(0f, 1f);
            float attackWeapon = w.Damage;
            float d;
            float bonus = get_bonus(player,w);
            d = (player.attackSpeed * 0.6f + attackWeapon * 0.4f) / 4f * (1 + modifiers + bonus);
            d = d - this.enemyBase.Defense / 10f;
            this.hp = this.hp - d;
            Debug.Log("Enemy damage is: " + d);
        }

        public Moves getRandomMove()
        {
            int r = Random.Range(0, enemyBase.Moves.Count);
            return enemyBase.Moves[r].Move;
        }

        public float get_spell_bonus(Spells.Spell spell, PlayerManager player)
        {
            float bonus = 0f;
            if (this.enemyBase.W2.Equals(spell.SpellBase.SpellName))
            {
                bonus += 1;
            }

            if (player.Inventory.getArtefact("Talisman"))
            {
                bonus += 1;
            }
            return bonus;
        }
        public float get_bonus(PlayerManager player,WeaponB w)
        {
            float bonus = 0f;
            if (enemyBase.W1.Equals(w.WeaponName))
            {
                bonus += 1;
            }

            if (player.Inventory.getArtefact("WarriorBook"))
            {
                bonus += 1;
            }
            return bonus;
        }
    }
}