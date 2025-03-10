using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public static Volume Instance;
    public float v=100;
    public Text counterText;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        v = PlayerPrefs.GetFloat("Volume", 0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counterText != null)
        {
            //v = PlayerPrefs.GetFloat("Volume", 0);
            counterText.text = v.ToString();
        }
    }
    public void Increase()
    {
        if (v < 100)
        {
            v += 1;
            Debug.Log(PlayerPrefs.GetFloat("Volume"));
           // PlayerPrefs.SetFloat("Volume", v);
           // PlayerPrefs.Save();
        }
    }
    public void Decrease()
    {
        if (v > 0)
        {
            v -= 1;
           // PlayerPrefs.SetFloat("Volume", v);
           // PlayerPrefs.Save();
          //  Debug.Log(PlayerPrefs.GetFloat("Volume"));
        }
    }
}
