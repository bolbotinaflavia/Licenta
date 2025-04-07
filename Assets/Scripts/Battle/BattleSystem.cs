using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
        public static BattleSystem instance;
        [SerializeField] PlayerManager player;
        [SerializeField] Hp_slider hpBar_player;
        [SerializeField] BattleUnit enemyUnit;
        [SerializeField] HPBar hpBar;
        private BattleState state;
        
        public BattleState State{get{return state;}set{state = value;}}

        void Start()
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
            hpBar_player.UpdateUI();
            
            yield return new WaitForSeconds(1f);

            PlayerActions();
        }

        void PlayerActions()
        {
            state = BattleState.PlayerAction;
        }

        void Update()
        {
            
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void HandleActionSelector(String name)
        {
            if (name == "attack")
            {
                Weapons attack_damage = player.attack();
                enemyUnit.Attacked(attack_damage);
                //nu merge animatia la hpbar
            
             //  Debug.Log("player attack is "+player.attack());
               // enemyUnit.Hp = enemyUnit.Hp - player.attack().ConvertTo<int>();
                hpBar.UpdateUI_Enemy();
                hpBar_player.UpdateUI();
            }

            if (name == "defense")
            {
                player.defense_battle(enemyUnit.attack());
               // player.HP-=player.HP-(enemyUnit.attack()+(int)(player.defense*0.1));
                hpBar_player.UpdateUI();
                hpBar.UpdateUI_Enemy();
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
            if(instance == null)
                instance = this;
           
        }

        public void exit_battle()
        {
            
        }
    }
}