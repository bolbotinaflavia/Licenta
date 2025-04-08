using Battle;
using Cinemachine;
using Player;
using Sliders_scripts;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameState
{
    FreeRoam,
    Battle
}
public class GameController:MonoBehaviour
{
    public static GameController Instance;
    public GameState state;
    [FormerlySerializedAs("player_movement")] [SerializeField] private PlayerManager playerMovement;
    [SerializeField] private BattleSystem battleSystem;
    [FormerlySerializedAs("battle_camera")] [SerializeField] private Camera battleCamera;
    [FormerlySerializedAs("player_camera")] [SerializeField] private CinemachineVirtualCamera playerCamera;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
        playerMovement.OnEncountered += StartBattle;
    }

    private void StartBattle()
    {
        state = GameState.Battle;
        playerCamera.gameObject.SetActive(false);
        playerCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = false;
        HpSlider.Instance.UpdateUI();
            
        battleSystem.gameObject.SetActive(true);
        battleCamera.gameObject.SetActive(true);
            
            
    }

    public void StopBattle()
    {
        Debug.Log("StopBattle");
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        battleCamera.gameObject.SetActive(false);
            
        playerCamera.gameObject.SetActive(true);
        playerCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = true;
        MenuManager.Instance.currentMenu.gameObject.SetActive(false);
        HpSlider.Instance.UpdateUI();

            
    }
    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerMovement.HandleUpdate();
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