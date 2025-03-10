using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_player_menu :Menu_countdown
{
    protected override void OnTimerComplete()
    {
        menu_option.value = 1;
        if (MenuManager.Instance.current_menu.activeSelf != true)
        {
            
            MenuManager.Instance.current_menu.SetActive(true);
        }

        else
        {
            
            MenuManager.Instance.current_menu.SetActive(false);
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
