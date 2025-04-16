using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        }
        else
        {
            Debug.LogWarning("No previous menu to return to.");
        }
    }

    private void InstantiateMenu(string menuName)
    {
        Debug.Log($"Attempting to load menu: {menuName}");

        GameObject menuPrefab = Resources.Load<GameObject>($"Menus/{menuName}");
        if (menuPrefab != null)
        {
            currentMenu = Instantiate(menuPrefab, transform);
            currentMenu.SetActive(true);
            Debug.Log($"Successfully instantiated '{menuName}'.");
        }
        else
        {
            Debug.LogError($"Menu '{menuName}' not found in Resources/Menus/.");
        }
    }

}
