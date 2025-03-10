using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_menu : Menu_countdown
{
    public string next_menu;
    protected override void OnTimerComplete()
    {
        menu_option.value = 1;
        MenuManager.Instance.LoadMenu(next_menu);
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
