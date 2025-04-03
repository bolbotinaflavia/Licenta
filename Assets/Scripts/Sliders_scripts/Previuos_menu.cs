using System.Collections;
using System.Collections.Generic;
using Battle;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Previuos_menu : Menu_countdown
{
    public bool isFight;
    protected override void OnTimerComplete()
    {
        menu_option.value = 1;
        if (GameController.Instance.state == GameState.Battle)
        {
            MenuManager.Instance.current_menu.SetActive(false);
            
        }
        else
        {
            MenuManager.Instance.BackToPrevious();
        }
        
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
