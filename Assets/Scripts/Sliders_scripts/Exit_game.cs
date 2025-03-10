using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_game : Menu_countdown
{
    public string next_menu;

    protected override void OnTimerComplete()
    {
        if (MenuManager.Instance != null)
        {
            SceneManager.LoadScene("StartGame");
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
