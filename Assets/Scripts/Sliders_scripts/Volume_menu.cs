using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume_menu : Menu_countdown
{
    private Volume v;
    public static Volume_menu Instance;
    public int inc_dec;
    public Text counterText;
    protected override void OnTimerComplete()
    {
        if (inc_dec > 0)
        {
            Volume.Instance.Increase();
            menu_option.value = 1;
            StartTimer();
        }
        else
        {
            Volume.Instance.Decrease();
            menu_option.value = 1;
            StartTimer();
        }
    }

    void Awake()
    {
        Instance = this;
        v= Volume.FindObjectOfType<Volume>();
        counterText.text = (v.globalVolume.weight * 100).ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        counterText.text=v.globalVolume.weight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (counterText != null)
        {
            //v = PlayerPrefs.GetFloat("Volume", 0);
            counterText.text = (v.globalVolume.weight*100).ToString();
            //counterText.text = (s.volume*100).ToString();
        }
    }
}
