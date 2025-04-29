using System;
using System.Collections;
using Enemies;
using Inventory;
using Player;
using Sliders_scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle
{
    public enum BattleState
    {
        Start,
        PlayerAction,
        PlayerMove,
        EnemyMove,
        Busy,
        EnemyDead
    }
    public class BattleSystem : MonoBehaviour
    {
        public static BattleSystem Instance;
        [SerializeField] private PlayerUnit player;
        [FormerlySerializedAs("hpBar_player")] [SerializeField]
        private HpSlider hpBarPlayer;
        [SerializeField] private BattleUnit enemyUnit;
        [SerializeField] private HpBar hpBar;
        [SerializeField] private string loadEnemyName;
        public string LoadEnemyName
        {
            get => loadEnemyName;
            set => loadEnemyName=value;
        }

        private BattleState state;

        public BattleState State
        {
            get => state;
            set => state=value;
        }

        public BattleUnit EnemyUnit
        {
            get => enemyUnit;
            set => enemyUnit = value;
        }
        private void Start()
        {
            // enemyUnit=this.GetComponent<BattleUnit>();
            // hpBar_player = this.GetComponent<Hp_slider>();
            // hpBar=this.GetComponent<HPBar>();
            // player = this.GetComponent<PlayerManager>();
           // StartCoroutine(Setup_battle());
        }

        public IEnumerator Setup_battle()
        {
            //player.Setup();
           // enemyUnit = gameObject.AddComponent<BattleUnit>();
       
            enemyUnit.Setup_Enemy(loadEnemyName);
         //    if(enemyUnit!=null)
           //      Debug.Log(" Enemy is: "+enemyUnit.name+" with hp: " + enemyUnit.Hp);
       
            hpBar.Setup(enemyUnit);
            hpBar.UpdateUI_Enemy();
            hpBarPlayer.UpdateUI();
            
            yield return new WaitForSeconds(1f);

            StartCoroutine(PlayerActions());
        }

        private IEnumerator PlayerActions()
        {
            state = BattleState.PlayerAction;
          
            yield return new WaitForSeconds(1f);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator PlayerActionMove(String actionName)
        {
            BattleSystem.Instance.State = BattleState.Busy;
            switch (actionName)
            {
                case "attack":
                {
                    var attackDamage = player.AttackBattle();
                    enemyUnit.Attacked(attackDamage,player.player);
                    new WaitForSeconds(2f);
                    hpBar.UpdateUI_Enemy();
                   
                    //nu merge animatia la hpbar
            
                    //  Debug.Log("player attack is "+player.attack());
                    // enemyUnit.Hp = enemyUnit.Hp - player.attack().ConvertTo<int>();
                    break;
                }
                case "defense":
                    //aici cresti defensul cu 10 cand te ataca
                    //de vazut aici
                    new WaitForSeconds(2f);
                    // player.HP-=player.HP-(enemyUnit.attack()+(int)(player.defense*0.1));
                  //  hpBarPlayer.UpdateUI();
                    break;
                //spell selection
                case"Brisingr":
                    var spell = InventoryManager.Instance.getSpell(actionName);
                    enemyUnit.AttackedBySpell(spell, player.player);
                    break;
                case "Jierda":
                    spell = InventoryManager.Instance.getSpell(actionName);
                    enemyUnit.AttackedBySpell(spell, player.player);
                    break;
                case"Slytha":
                    spell = InventoryManager.Instance.getSpell(actionName);
                    enemyUnit.AttackedBySpell(spell, player.player);
                    break;
                case"Poison":
                    spell = InventoryManager.Instance.getSpell(actionName);
                    enemyUnit.AttackedBySpell(spell, player.player);
                    break;
                
            }
            if (enemyUnit.Hp <= 0)
            {
                yield return player.player.notification_show("The enemy is dead");
                state = BattleState.EnemyDead;
            }
            else
            {
                new WaitForSeconds(5f);
                StartCoroutine(EnemyMove());
            }
        }

        private IEnumerator EnemyMove()
        {
            state = BattleState.EnemyMove;
            var move = enemyUnit.getRandomMove();
            var damage=enemyUnit.Attack(move,player.player);
            player.player.hp -= damage;
            new WaitForSeconds(2f);
            // player.HP-=player.HP-(enemyUnit.attack()+(int)(player.defense*0.1));
            player.HpAnim.damaging_animation();
            hpBarPlayer.UpdateUI();
           // hpBar.UpdateUI_Enemy();
            
            if (player.player.hp <= 0)
            {
                yield return player.player.notification_show("The player is dead");
                state = BattleState.EnemyDead;
            }
            else
            {
                StartCoroutine(PlayerActions());
            }
        }
        
        public void HandleUpdate()
        {
            if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove"))
            {
                PlayerMovement.Instance.CurrentControl.select_sliders();
            }
            if (state == BattleState.PlayerAction)
            {
                //se face din slider_scripts actiunea
            }

            if (state == BattleState.PlayerMove)
            {
                //aici animatia
            }

            if (state == BattleState.EnemyDead)
            {
                Invoke(nameof(exit_battle),3f);
            }
            // if (enemyUnit!=null&&enemyUnit.Hp == 0)
            // {
            //     GameController.Instance.StopBattle();
            // }
            //
            // if (player!=null&& player.Hp.Equals(0))
            // {
            //     GameController.Instance.StopBattle();
            //     //exit game in theory+ animation and try again
            // }
        }

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
           
        }

        public void exit_battle()
        {
            GameController.Instance.StopBattle();
        }
    }
}