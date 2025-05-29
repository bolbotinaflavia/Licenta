using System;
using System.Collections;
using DefaultNamespace;
using Enemies;
using Inventory;
using Player;
using Sliders_scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        EnemyDead,
        PlayerDead,
    }
    public class BattleSystem : MonoBehaviour
    {
        public static BattleSystem Instance;
        [SerializeField] private BattleAnimationManager animationManager;
        [SerializeField] private PlayerUnit player;
        [FormerlySerializedAs("hpBar_player")] [SerializeField]
        private HpSlider hpBarPlayer;
        [SerializeField] private BattleUnit enemyUnit;
        [SerializeField] private HpBar hpBar;
        [SerializeField] private string loadEnemyName;
        [SerializeField] private Notification _notification;
        public Notification Notification => _notification;
        public BattleAnimationManager AnimationManager => animationManager;
        public string LoadEnemyName
        {
            get => loadEnemyName;
            set => loadEnemyName=value;
        }

        [SerializeField] private BattleState state;

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
            player.Setup();
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
            StartCoroutine(_notification.notification_show("It's your turn!!",5f));
            hpBar.UpdateUI_Enemy();
            hpBarPlayer.UpdateUI();
            yield return new WaitForSeconds(1f);
        }

        private IEnumerator spell_battle(string actionName)
        {
            StartCoroutine(animationManager.startAnimationsPlayerAttack());
            new WaitForSeconds(5f);
            yield return new WaitForSeconds(5f);
            state = BattleState.EnemyMove;
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator PlayerActionMove(string actionName)
        {
            BattleSystem.Instance.State = BattleState.Busy;
           // yield return new WaitForSeconds(2f);
            switch (actionName)
            {
                case "attack":
                {
                    var attackDamage = player.AttackBattle();
                    enemyUnit.Attacked(attackDamage,player.player);
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
                    StartCoroutine(spell_battle(actionName));
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
                StartCoroutine(enemyDefeted());
                state = BattleState.EnemyDead;
            }
            else
            {
                new WaitForSeconds(5f);
                StartCoroutine(_notification.notification_show($"You used {actionName}", 2f));
                yield return StartCoroutine(animationManager.startAnimationsPlayerAttack());
                new WaitForSeconds(2f);
                state = BattleState.EnemyMove;
            }
            yield return new WaitForSeconds(6f);
        }

        private IEnumerator EnemyMove()
        {
            //state = BattleState.EnemyMove;
            new WaitForSeconds(3f);
            StartCoroutine(_notification.notification_show("Enemy turn!!",4f));
            yield return new WaitForSeconds(2f);
            var move = enemyUnit.getRandomMove(); 
            enemyUnit.Attack(move,player.player);
           
            // player.HP-=player.HP-(enemyUnit.attack()+(int)(player.defense*0.1));
           
           // hpBar.UpdateUI_Enemy();
            
            if (player.player.hp <= 0)
            {
                StartCoroutine(playerDefeted());
                state = BattleState.PlayerDead;
            }
            else
            {
                StartCoroutine(animationManager.startAnimationsEnemyAttack());
                StartCoroutine(_notification.notification_show($"Enemy used {move.MoveName}",4f));
                yield return new WaitForSeconds(6f);
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

            if (state == BattleState.EnemyMove)
            {
                StopAllCoroutines();
                StartCoroutine(EnemyMove());
                state = BattleState.Busy;
            }

            if (state == BattleState.EnemyDead)
            {
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

        private IEnumerator enemyDefeted()
        {
            state = BattleState.Busy;
            StartCoroutine(_notification.notification_show($"Enemy Defeated",4f));
            //maybe ceva animatie
            yield return new WaitForSeconds(2f);
            exit_battle(true);
        }

        private IEnumerator playerDefeted()
        {
            state = BattleState.Busy;
            StartCoroutine(_notification.notification_show($"Player Defeated",4f));
            exit_battle(false);
            Destroy(PlayerManager.Instance.gameObject);
            MenuManager.Instance.BackToPrevious();
            Destroy(Volume.Instance.gameObject);
            MenuManager.Instance.LoadMenu("GameOver");
            SceneManager.LoadScene("GameOver");
            //maybe ceva animatie
            yield return new WaitForSeconds(2f);
        }
        public void exit_battle(bool winner)
        {
            GameController.Instance.StopBattle(winner);
            state = BattleState.Busy;
        }
        
    }
}