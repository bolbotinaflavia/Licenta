using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_game : Menu_countdown
{
    protected override void OnTimerComplete()
    {
        Debug.Log("quitting....");
        Application.Quit();
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
