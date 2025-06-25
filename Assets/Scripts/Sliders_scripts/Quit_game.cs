using UnityEngine;

public class QuitGame : MenuCountdown
{
    protected override void OnTimerComplete()
    {
        Debug.Log("quitting....");
        Application.Quit();
        menuOption.value = 1;
          StartCoroutine(Deselect());
    }

    // Start is called before the first frame update
   
}