using System;
using System.Collections;
using Enemies;
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
        Busy
    }
    public class BattleSystem : MonoBehaviour
    {
        public static BattleSystem Instance;
        [SerializeField] private PlayerManager player;
        [FormerlySerializedAs("hpBar_player")] [SerializeField]
        private HpSlider hpBarPlayer;
        [SerializeField] private BattleUnit enemyUnit;
        [SerializeField] private HpBar hpBar;

        public BattleState State { get; private set; }

        private void Start()
        {
            // enemyUnit=this.GetComponent<BattleUnit>();
            // hpBar_player = this.GetComponent<Hp_slider>();
            // hpBar=this.GetComponent<HPBar>();
            // player = this.GetComponent<PlayerManager>();
            StartCoroutine(Setup_battle());
        }
        public IEnumerator Setup_battle()
        {
            //player.Setup();
           // enemyUnit = gameObject.AddComponent<BattleUnit>();
       
            enemyUnit.Setup_Enemy();
         //    if(enemyUnit!=null)
           //      Debug.Log(" Enemy is: "+enemyUnit.name+" with hp: " + enemyUnit.Hp);
       
            hpBar.Setup(enemyUnit);
            hpBar.UpdateUI_Enemy();
            hpBarPlayer.UpdateUI();
            
            yield return new WaitForSeconds(1f);

            PlayerActions();
        }

        private void PlayerActions()
        {
            State = BattleState.PlayerAction;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void HandleActionSelector(String actionName)
        {
            switch (actionName)
            {
                case "attack":
                {
                    var attackDamage = player.Attack();
                    enemyUnit.Attacked(attackDamage);
                    //nu merge animatia la hpbar
            
                    //  Debug.Log("player attack is "+player.attack());
                    // enemyUnit.Hp = enemyUnit.Hp - player.attack().ConvertTo<int>();
                    hpBar.UpdateUI_Enemy();
                    hpBarPlayer.UpdateUI();
                    break;
                }
                case "defense":
                    player.defense_battle(enemyUnit.Attack());
                    // player.HP-=player.HP-(enemyUnit.attack()+(int)(player.defense*0.1));
                    hpBarPlayer.UpdateUI();
                    hpBar.UpdateUI_Enemy();
                    break;
            }
        }
        
        public void HandleUpdate()
        {
            // if (enemyUnit.Hp == 0)
            // {
            //     GameController.Instance.StopBattle();
            // }
            //
            // if (player.HP.Equals(0))
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
            
        }
    }
}