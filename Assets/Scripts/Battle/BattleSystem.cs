using System;
using System.Collections;
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
            StartCoroutine(Setup_battle());
        }
        public IEnumerator Setup_battle()
        {
            //player.Setup();
            enemyUnit.Setup();
            hpBar_player.UpdateUI();
            hpBar.Setup(enemyUnit);
            hpBar.UpdateUI();
            
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

        public void HandleActionSelector(String name)
        {
            if (name == "attack")
            {
                hpBar.hp_slider.value -= player.attack().ConvertTo<int>();
                hpBar.UpdateUI();
                hpBar_player.UpdateUI();
            }

            if (name == "defense")
            {
                player.HP-=hpBar_player.hp_slider.value-(enemyUnit.enemy.Attack.ConvertTo<int>()+player.defense*10);
                hpBar_player.UpdateUI();
                hpBar.UpdateUI();
            }
        }
        
        public void HandleUpdate()
        {
            
        }

        private void Awake()
        {
            if(instance == null)
                instance = this;
           
        }
    }
}