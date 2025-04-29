using UnityEngine;

public class QuitGame : MenuCountdown
{
    protected override void OnTimerComplete()
    {
        Debug.Log("quitting....");
        Application.Quit();
    }

    // Start is called before the first frame update
   
}
