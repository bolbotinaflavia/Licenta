using Battle;
using Cinemachine;
using Enemies;
using Inventory;
using Player;
using Sliders_scripts;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum GameState
{
    FreeRoam,
    Battle,
    Menu,
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
    [SerializeField] private GameObject enemy_obj;
    public GameObject EnemyObj
    {
        get => enemy_obj;
        set => enemy_obj = value;
    }
    private float timeTaken;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (playerMovement == null)
            playerMovement = PlayerManager.Instance;

        playerMovement.OnEncountered += StartBattle;
    }
    private void StartBattle(Enemy enemy,GameObject obj)
    {
        timeTaken = Time.time;
        state = GameState.Battle;
        if(audioIdle!=null)
            audioIdle.Stop();
        if(audioBattle!=null)
            audioBattle.PlayDelayed(2f);
        PlayerManager.Instance.IsMoving = false;
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
        enemy_obj = obj;
        if(audioIdle!=null)
            audioIdle.mute = true;
        StartCoroutine(battleSystem.Setup_battle());
    }
    
    public void StopBattle(bool enemyDefeated)
    {
        if (enemyDefeated)
        {
             UnityServices.InitializeAsync();
            EnemyDefeated newEvent= new EnemyDefeated();
            newEvent.enemyName = enemy_obj.GetComponent<Enemy>().EnemieBase.name;
            newEvent.number = 1; // Assuming 1 for now, can be changed based on actual logic
            newEvent.timeToDefeat= (Time.time- timeTaken)/60f;
            AnalyticsService.Instance.RecordEvent(newEvent);
            if(enemy_obj.GetComponent<Enemy>().InsideObject!=null)
            {
                enemy_obj.GetComponent<Enemy>().InsideObject.SetActive(true);
                var insideObj = enemy_obj.GetComponent<Enemy>().InsideObject;
                InventoryManager.Instance.add_object(insideObj);
            }
            Destroy(enemy_obj);
        }
        else
        {
            playerMovement.player.transform.position= new Vector3(PlayerManager.Instance.player.transform.position.x +150f, PlayerManager.Instance.player.transform.position.y, 0);
        }
        state = GameState.FreeRoam;
        PlayerManager.Instance.IsMoving = true;
        if(audioBattle!=null)
            audioBattle.Stop();
        if (audioIdle != null)
        {
            audioIdle.Play();
            audioIdle.mute = false;
        }
        battleSystem.gameObject.SetActive(false);
        battleCamera.gameObject.SetActive(false);
        
        playerCamera.gameObject.SetActive(true);
        playerCamera.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = true;
        MenuManager.Instance.current.gameObject.SetActive(false);
        HpSlider.Instance.UpdateUI();
    }
    
    private void Update()
    {
        // if (SceneManager.GetActiveScene().name == "Level2")
        // {
        //     if(battleSystem==null)
        //          update_gameobjects();
        // }
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