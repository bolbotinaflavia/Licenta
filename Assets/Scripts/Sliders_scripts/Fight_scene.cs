using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_scene : Menu_countdown
{
    public int actiune;

    protected override void OnTimerComplete()
    {
        //attack
        if (actiune == 1)
            Debug.Log("Attack");
        //defense
        if (actiune == 2)
            Debug.Log("Defense");
        //spell
        if (actiune == 3)
            Debug.Log("Spell");

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
