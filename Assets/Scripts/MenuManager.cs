using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [FormerlySerializedAs("game_started")] public bool gameStarted;

    [FormerlySerializedAs("current_menu")] public GameObject currentMenu;
    private Stack<GameObject> _menuHistory = new Stack<GameObject>(); // Store menu names

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        LoadMenu(!gameStarted ? "Menu_start" : "Menu_basic"); // Load initial menu
    }

    /*public void LoadGame()
    {
        game_started = true;
        //LoadMenu("Menu_basic");
        if (current_menu != null)
            current_menu.SetActive(false);
        else
            Debug.Log("mAIN MENU couldn't load");
        SceneManager.LoadScene("Gameplay");
    }*/

    public void LoadMenu(string menuName)
    {
        if (currentMenu != null)
        {
            _menuHistory.Push(currentMenu); // Store previous menu name
            foreach (var s in GameObject.FindObjectsOfType<Slider>())
            {
                if (!s.name.Equals("OpenMenu")&&!s.name.Equals("HP") && !s.tag.Equals("HP"))
                    s.value = 1;
            }
            if (PlayerMovement.Instance.CurrentControl.get_action().name.Equals("KeyboardMove"))
            {
                if (GameController.Instance != null)
                {
                    if(GameController.Instance.state==GameState.Menu)
                        PlayerMovement.Instance.CurrentControl.load_sliders();
                }
                else
                {
                    PlayerMovement.Instance.CurrentControl.load_sliders();
                }
            }
            currentMenu.SetActive(false);
        }
        InstantiateMenu(menuName);
    }

    public void battlePreviousMenu()
    {
        if (!currentMenu.gameObject.name.Equals("Menu_basic(Clone)"))
        {
            Destroy(currentMenu);
            GameObject previousMenu = _menuHistory.Pop();
            if (previousMenu.gameObject.name.Equals("Menu_basic(Clone)"))
            {
                currentMenu = previousMenu;
            }
            else
            {
                previousMenu.SetActive(true);
                currentMenu = previousMenu;
                foreach (var s in GameObject.FindObjectsOfType<Slider>())
                {
                    if (!s.name.Equals("OpenMenu")&&!s.name.Equals("HP") && !s.tag.Equals("HP"))
                        s.value = 1;
                }
            }
        }
    }

    public void BackToPrevious()
    {
        if (_menuHistory.Count > 0)
        {
            Destroy(currentMenu);
            GameObject previousMenu = _menuHistory.Pop();
            previousMenu.SetActive(true);
            currentMenu = previousMenu;
            foreach (var s in GameObject.FindObjectsOfType<Slider>())
            {
                if (!s.name.Equals("OpenMenu")&&!s.name.Equals("HP") && !s.tag.Equals("HP"))
                    s.value = 1;
            }
        }
        else
        {
            Debug.LogWarning("No previous menu to return to.");
        }
    }

    private void InstantiateMenu(string menuName)
    {
        GameObject menuPrefab = Resources.Load<GameObject>($"Menus/{menuName}");
        if (menuPrefab != null)
        {
            currentMenu = Instantiate(menuPrefab, transform);
            currentMenu.SetActive(true);
        }
        else
        {
            Debug.LogError($"Menu '{menuName}' not found in Resources/Menus/.");
        }
    }

}
