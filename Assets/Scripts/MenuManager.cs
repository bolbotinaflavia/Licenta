using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [FormerlySerializedAs("game_started")] public bool gameStarted;

    [FormerlySerializedAs("currentMenu")] [FormerlySerializedAs("current_menu")] public GameObject current;
    private Stack<GameObject> _menuStack = new Stack<GameObject>(); // Store menu names

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
    public void LoadMenu(string menuName)
    {
        if (current != null)
        {
            _menuStack.Push(current); // Store previous menu name
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
            current.SetActive(false);
        }
        InstantiateNewMenu(menuName);
    }

    public void battlePreviousMenu()
    {
        if (!current.gameObject.name.Equals("Menu_basic(Clone)"))
        {
            Destroy(current);
            GameObject previousMenu = _menuStack.Pop();
            if (previousMenu.gameObject.name.Equals("Menu_basic(Clone)"))
            {
                current = previousMenu;
            }
            else
            {
                previousMenu.SetActive(true);
                current = previousMenu;
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
        if (_menuStack.Count > 0)
        {
            Destroy(current);
            GameObject previousMenu = _menuStack.Pop();
            previousMenu.SetActive(true);
            current = previousMenu;
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
    public void LoadPrevious()
    {
        if (_menuStack.Count > 0)
        {
            Destroy(current);
            GameObject previousMenu = _menuStack.Pop();
            previousMenu.SetActive(true);
            current = previousMenu;
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

    private void InstantiateNewMenu(string menuName)
    {
        GameObject menuPrefab = Resources.Load<GameObject>($"Menus/{menuName}");
        if (menuPrefab != null)
        {
            current = Instantiate(menuPrefab, transform);
            current.SetActive(true);
        }
        else
        {
            Debug.LogError($"Menu '{menuName}' not found in Resources/Menus/.");
        }
    }

}
