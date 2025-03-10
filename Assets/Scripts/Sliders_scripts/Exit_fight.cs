using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_fight : Menu_countdown
{
    protected override void OnTimerComplete()
    {
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
