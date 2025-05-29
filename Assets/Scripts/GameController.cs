using System.Collections;
using System.IO.Enumeration;
using Battle;
using Cinemachine;
using Enemies;
using Eyeware.BeamEyeTracker.Unity;
using Inventory;
using Player;
using Sliders_scripts;
using Unity.VisualScripting;
using UnityEngine;
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

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        if(playerMovement==null)
            playerMovement = PlayerManager.Instance;
       
        playerMovement.OnEncountered += StartBattle;
    }
    private void StartBattle(Enemy enemy,GameObject obj)
    {
        state = GameState.Battle;
        if(audioIdle!=null)
            audioIdle.Stop();
        if(audioBattle!=null)
            audioBattle.PlayDelayed(2f);
        PlayerManager.Instance.isMoving = false;
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
            enemy_obj.GetComponent<Enemy>().InsideObject.SetActive(true);
            var insideObj = enemy_obj.GetComponent<Enemy>().InsideObject;
            if(insideObj!=null)
                InventoryManager.Instance.add_object(insideObj);
            Destroy(enemy_obj);
        }
        else
        {
            playerMovement.player.transform.position += new Vector3(enemy_obj.transform.position.x +100f, 0, 0);
        }
        Debug.Log("StopBattle");
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
        MenuManager.Instance.currentMenu.gameObject.SetActive(false);
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