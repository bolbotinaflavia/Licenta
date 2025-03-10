using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public bool game_started = false;

    public GameObject current_menu;
    private Stack<GameObject> menuHistory = new Stack<GameObject>(); // Store menu names

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


        if (!game_started)
        {
            LoadMenu("Menu_start"); // Load initial menu
        }
        else
        {
            LoadMenu("Menu_basic");
        }
       
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

    public void LoadMenu(string menu_name)
    {
        if (current_menu != null)
        {
            menuHistory.Push(current_menu); // Store previous menu name
            current_menu.SetActive(false);
        }

        InstantiateMenu(menu_name);
        
    }

    public void BackToPrevious()
    {
        if (menuHistory.Count > 0)
        {
            Destroy(current_menu);
            GameObject previousMenu = menuHistory.Pop();
            previousMenu.SetActive(true);
            current_menu = previousMenu;
        }
        else
        {
            Debug.LogWarning("No previous menu to return to.");
        }
    }

    private void InstantiateMenu(string menu_name)
    {
        Debug.Log($"Attempting to load menu: {menu_name}");

        GameObject menuPrefab = Resources.Load<GameObject>($"Menus/{menu_name}");
        if (menuPrefab != null)
        {
            current_menu = Instantiate(menuPrefab, transform);
            current_menu.SetActive(true);
            Debug.Log($"Successfully instantiated '{menu_name}'.");
        }
        else
        {
            Debug.LogError($"Menu '{menu_name}' not found in Resources/Menus/.");
        }
    }

}
