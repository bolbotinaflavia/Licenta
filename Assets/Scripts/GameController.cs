using Battle;
using Cinemachine;
using Enemies;
using Player;
using Sliders_scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameState
{
    FreeRoam,
    Battle,
    Menu
}
public class GameController:MonoBehaviour
{
    public static GameController Instance;
    public AudioSource audioIdle;
    public AudioSource audioBattle;
    public GameState state;
    [FormerlySerializedAs("player_movement")] [SerializeField] private PlayerManager playerMovement;
    [SerializeField] private BattleSystem battleSystem;
    [FormerlySerializedAs("battle_camera")] [SerializeField] private Camera battleCamera;
    [FormerlySerializedAs("player_camera")] [SerializeField] private CinemachineVirtualCamera playerCamera;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        playerMovement.OnEncountered += StartBattle;
    }

    private void StartBattle(Enemy enemy)
    {
        state = GameState.Battle;
        audioIdle.Stop();
        audioBattle.PlayDelayed(2f);
        playerCamera.gameObject.SetActive(false);
        playerCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = false;
        HpSlider.Instance.UpdateUI();
        battleSystem.gameObject.SetActive(true);
        battleCamera.gameObject.SetActive(true);
        battleSystem.LoadEnemyName = enemy.EnemieBase.name;
        if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove"))
        {
            PlayerMovement.Instance.CurrentControl.load_sliders();
        }
        audioIdle.mute = true;
        StartCoroutine(battleSystem.Setup_battle());
    }

    public void StopBattle()
    {
        Debug.Log("StopBattle");
        state = GameState.FreeRoam;
        PlayerManager.Instance.IsMoving = true;
        audioBattle.Stop();
        audioIdle.Play();
        audioIdle.mute = false;
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
            else
            {
                if (state == GameState.Menu)
                {
                    playerMovement.HandleUpdate();
                  
                        PlayerMovement.Instance.CurrentControl.UpdateUI();
                }
            }
        }
        
    }
}