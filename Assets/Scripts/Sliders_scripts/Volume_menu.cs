using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume_menu : Menu_countdown
{
    public int inc_dec;
    protected override void OnTimerComplete()
    {
        if (inc_dec > 0)
        {
            Volume.Instance.Increase();
            menu_option.value = 1;
        }
        else
        {
            Volume.Instance.Decrease();
            menu_option.value = 1;
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
