using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Select_item : Menu_countdown
{
   // public static Select_item Instance;
    public string name;
    private Item i;
    public Text t;
    protected override void OnTimerComplete()
    {
       
        if (i != null)
        {
            i.consume();
        }

        Debug.Log("Item selected");
    }
    

    // Start is called before the first frame update
    void Start()
    {
        i=PlayerManager.Instance.objects.Find(item => item.name.Equals(name));
        if(i!=null)
            t.text = i.number.ToString();
        else
        {
            t.text = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (i != null)
        {
            t.text = i.number.ToString();
        }
    }
}
