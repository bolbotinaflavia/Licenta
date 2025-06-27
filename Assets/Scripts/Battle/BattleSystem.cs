using System;
using System.Collections;
using Animations;
using DefaultNamespace;
using Enemies;
using HPBar;
using HPBar;
using Inventory;
using Player;
using Sliders_scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Collections.Generic;

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
        public Dictionary<string, int> Move;
        public string LoadEnemyName
        {
            get => loadEnemyName;
            set => loadEnemyName = value;
        }

        [SerializeField] private BattleState state;

        public BattleState State
        {
            get => state;
            set => state=value;
        }
        public PlayerUnit Player
        {
            get => player;
            set => player = value;
        }
        public BattleUnit EnemyUnit
        {
            get => enemyUnit;
            set => enemyUnit = value;
        }
        public HpSlider HpBarPlayer
        {
            get => hpBarPlayer;
        }
        public HpBar HpBar
        {
            get => hpBar;
            set => hpBar = value;
        }
        private void Start()
        {
            // enemyUnit=this.GetComponent<BattleUnit>();
            // hpBar_player = this.GetComponent<Hp_slider>();
            // hpBar=this.GetComponent<HPBar>();
            // player = this.GetComponent<PlayerManager>();
            // StartCoroutine(Setup_battle());
            // StartCoroutine(Setup_battle());
        }

        public IEnumerator Setup_battle()
        {
           Move= new Dictionary<string, int>
        {
            { "Attack", 0 },
            { "Brisingr", 0 },
            { "Jierda", 0 },
            { "Slytha", 0 },
            { "Poison", 0 }
        };
           Move= new Dictionary<string, int>
        {
            { "Attack", 0 },
            { "Brisingr", 0 },
            { "Jierda", 0 },
            { "Slytha", 0 },
            { "Poison", 0 }
        };
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
        private IEnumerator spell_battle()
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
            Debug.Log(actionName);
            bool success = false;
            // yield return new WaitForSeconds(2f);
            switch (actionName)
            {
                case "attack":
                    {
                        var attackDamage = player.AttackBattle();
                        success = enemyUnit.Attacked(attackDamage, player.player);
                        Move["Attack"]++;

                        //nu merge animatia la hpbar

                        //  Debug.Log("player attack is "+player.attack());
                        // enemyUnit.Hp = enemyUnit.Hp - player.attack().ConvertTo<int>();
                        break;
                    }
                case "Brisingr":
                    {

                        var spell = InventoryManager.Instance.getSpell(actionName);
                        Debug.Log("attack with:" + spell.SpellBase.SpellName);
                        success = enemyUnit.AttackedBySpell(spell, player.player);
                        Move["Brisingr"]++;
                        StartCoroutine(spell_battle());
                        break;
                    }
                case "Jierda":
                    {
                        var spell = InventoryManager.Instance.getSpell(actionName);
                        success = enemyUnit.AttackedBySpell(spell, player.player);
                        Move["Jierda"]++;
                        StartCoroutine(spell_battle());
                        break;
                    }
                case "Slytha":
                    {
                        var spell = InventoryManager.Instance.getSpell(actionName);
                        success = enemyUnit.AttackedBySpell(spell, player.player);
                        Move["Slytha"]++;
                        StartCoroutine(spell_battle());
                        break;
                    }
                case "Poison":
                    {
                        var spell = InventoryManager.Instance.getSpell(actionName);
                        success = enemyUnit.AttackedBySpell(spell, player.player);
                        Move["Poison"]++;
                        StartCoroutine(spell_battle());
                        break;
                    }
                default:
                    {
                        Debug.LogError($"Unknown action: {actionName}");
                        StartCoroutine(_notification.notification_show($"Unknown action", 2f));
                        yield return new WaitForSeconds(2f);
                        state = BattleState.EnemyMove;
                        yield break;
                    }
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
               
                if (success == false)
                {
                    new WaitForSeconds(2f);
                    StartCoroutine(_notification.notification_show($"{actionName} failed", 2f));
                }
                else
                {
                    yield return StartCoroutine(animationManager.startAnimationsPlayerAttack());
                }
                Debug.Log("changing state");
               
                if (success == false)
                {
                    new WaitForSeconds(2f);
                    StartCoroutine(_notification.notification_show($"{actionName} failed", 2f));
                }
                else
                {
                    yield return StartCoroutine(animationManager.startAnimationsPlayerAttack());
                }
                Debug.Log("changing state");
                state = BattleState.EnemyMove;
                Debug.Log(state);
                Debug.Log(state);
            }
            
            
            
            
            yield return new WaitForSeconds(6f);
        }

        private IEnumerator EnemyMove()
        {
            Debug.Log("enemy move is started");
            Debug.Log("enemy move is started");
            //state = BattleState.EnemyMove;
            bool success = false;
            new WaitForSeconds(3f);
            StartCoroutine(_notification.notification_show("Enemy turn!!",4f));
            yield return new WaitForSeconds(2f);
            var move = enemyUnit.getRandomMove(); 
            success=enemyUnit.Attack(move,player.player);
            success=enemyUnit.Attack(move,player.player);
           
            // player.HP-=player.HP-(enemyUnit.attack()+(int)(player.defense*0.1));
           
           // hpBar.UpdateUI_Enemy();
            
            if (player.player.hp <= 0)
            {
                StartCoroutine(playerDefeted());
                state = BattleState.PlayerDead;
            }
            else
            {
                 new WaitForSeconds(5f);
                StartCoroutine(_notification.notification_show($"Enemy used {move.MoveName}",2f));
                if (success == false)
                {
                    //new WaitForSeconds(2f);
                    yield return new WaitForSeconds(3f);
                    StartCoroutine(_notification.notification_show($"{move.MoveName} failed", 2f));

                }
                else
                {
                    yield return StartCoroutine(animationManager.startAnimationsEnemyAttack());
                }
                yield return new WaitForSeconds(2f);
                 new WaitForSeconds(5f);
                StartCoroutine(_notification.notification_show($"Enemy used {move.MoveName}",2f));
                if (success == false)
                {
                    //new WaitForSeconds(2f);
                    yield return new WaitForSeconds(3f);
                    StartCoroutine(_notification.notification_show($"{move.MoveName} failed", 2f));

                }
                else
                {
                    yield return StartCoroutine(animationManager.startAnimationsEnemyAttack());
                }
                yield return new WaitForSeconds(2f);
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
                Debug.Log("enemy move is starting");
                Debug.Log("enemy move is starting");
                StopAllCoroutines();
                StartCoroutine(EnemyMove());
                state = BattleState.Busy;
            }

            if (state == BattleState.Busy)
            {

            }
            if (state == BattleState.Busy)
            {

            }
            if (state == BattleState.EnemyDead)
            {
                if (enemyUnit.Hp <= 0)
                    StartCoroutine(enemyDefeted());
                else
                    state = BattleState.Busy;
            }
            if(state==BattleState.PlayerDead)
            {
                if (player.Hp <= 0)
                    StartCoroutine(playerDefeted());
                else
                    state = BattleState.Busy;
                if (enemyUnit.Hp <= 0)
                    StartCoroutine(enemyDefeted());
                else
                    state = BattleState.Busy;
            }
            if(state==BattleState.PlayerDead)
            {
                if (player.Hp <= 0)
                    StartCoroutine(playerDefeted());
                else
                    state = BattleState.Busy;
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
            UnityServices.InitializeAsync();
            foreach (var m in Move)
            {
                MoveUsedEvent moveEvent = new MoveUsedEvent();
                moveEvent.moveName = m.Key;
                moveEvent.number = m.Value;
                if( m.Value > 0)
                  AnalyticsService.Instance.RecordEvent(moveEvent);
            }
            UnityServices.InitializeAsync();
            foreach (var m in Move)
            {
                MoveUsedEvent moveEvent = new MoveUsedEvent();
                moveEvent.moveName = m.Key;
                moveEvent.number = m.Value;
                if( m.Value > 0)
                  AnalyticsService.Instance.RecordEvent(moveEvent);
            }
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
            MenuManager.Instance.LoadPrevious();
            Destroy(VolumeManager.Instance.gameObject);
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