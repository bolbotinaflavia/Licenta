using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_game: Menu_countdown
{
    public string next_menu;

    protected override void OnTimerComplete()
    {
        menu_option.value = 1;
        MenuManager.Instance.LoadMenu(next_menu);
        MenuManager.Instance.game_started = true;
        MenuManager.Instance.current_menu.SetActive(false);
        SceneManager.LoadScene("Gameplay");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
