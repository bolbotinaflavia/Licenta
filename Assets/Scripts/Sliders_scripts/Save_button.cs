using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_button : Menu_countdown
{
    protected override void OnTimerComplete()
    {
        //PlayerPrefs.SetFloat("Volume", Volume.Instance.s.volume);
        PlayerPrefs.Save();
        menu_option.value = 1;
        MenuManager.Instance.BackToPrevious();
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
