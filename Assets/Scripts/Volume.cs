using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public static Volume Instance;
    public AudioSource s;
    //public float v;
    public Text counterText;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        s=GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        if (s == null)
        {
            counterText.text = "0";
            
        }
        counterText.text = (s.volume*100).ToString();
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
            counterText.text = (s.volume*100).ToString();
        }
    }
    public void Increase()
    {
        if (s.volume*100 < 100)
        {
            s.volume += 0.05f;
            Debug.Log(PlayerPrefs.GetFloat("Volume"));
           // PlayerPrefs.SetFloat("Volume", v);
           // PlayerPrefs.Save();
        }
    }
    public void Decrease()
    {
        if (s.volume*100 > 0)
        {
            s.volume -= 0.05f;
           // PlayerPrefs.SetFloat("Volume", v);
           // PlayerPrefs.Save();
          //  Debug.Log(PlayerPrefs.GetFloat("Volume"));
        }
    }
}
