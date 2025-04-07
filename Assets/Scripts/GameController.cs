using System;
using Battle;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public enum GameState
    {
        FreeRoam,
        Battle
    }
    public class GameController:MonoBehaviour
    {
        public static GameController Instance;
        public GameState state;
        [SerializeField] PlayerManager player_movement;
        [SerializeField] BattleSystem battleSystem;
        [SerializeField] private Camera battle_camera;
        [SerializeField] private CinemachineVirtualCamera player_camera;

        private void Start()
        {
            if(Instance == null)
                Instance = this;
            player_movement.OnEncountered += StartBattle;
        }

        void StartBattle()
        {
            state = GameState.Battle;
            player_camera.gameObject.SetActive(false);
            player_camera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = false;
            Hp_slider.Instance.UpdateUI();
            
            battleSystem.gameObject.SetActive(true);
            battle_camera.gameObject.SetActive(true);
            
            
        }

        public void StopBattle()
        {
            Debug.Log("StopBattle");
            state = GameState.FreeRoam;
            battleSystem.gameObject.SetActive(false);
            battle_camera.gameObject.SetActive(false);
            
            player_camera.gameObject.SetActive(true);
            player_camera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = true;
            MenuManager.Instance.current_menu.gameObject.SetActive(false);
            Hp_slider.Instance.UpdateUI();

            
        }
        private void Update()
        {
            if (state == GameState.FreeRoam)
            {
                player_movement.HandleUpdate();
            }
            else
            {
                if (state == GameState.Battle)
                {
                    battleSystem.HandleUpdate();
                }
            }
        }
    }
}