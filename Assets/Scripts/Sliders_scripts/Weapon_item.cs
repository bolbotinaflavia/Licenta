using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Weapons_menu : Menu_countdown
{
    public String name;
    private Weapons weapon=null;
    public UnityEngine.UI.Image img;
    //public Sprite weaponSprite;

    protected override void OnTimerComplete()
    {
        if (weapon != null&&weapon.IsDiscovered())
        {
            SelectWeapon();
            UpdateUI();
        }
    }

    public void FindWeaponsInGame()
    {
        weapon= GameObject.Find(name).ConvertTo<Weapons>(); ;
    }



    // Start is called before the first frame update
    void Start()
    {
        //weapon = w.gameObject.GetComponent<Weapons>();
        FindWeaponsInGame();
        UpdateUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectWeapon()
    {
        if (weapon.InUse == false)
        {
            
            Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);
            Color c_back = new Color(0.4901961F,0.4392157F,0.4431373F);

            foreach (var w in PlayerManager.Instance.weapons)
            {
                w.SetInhUse(false);
               
            }
            foreach(var s in FindObjectsOfType<Slider>())
            {
                if (s.IsActive())
                {
                    if (!s.name.Equals("Back"))
                       s.fillRect.GetComponent<Image>().color = c;
                }
            }
            //weapon.InUse = true;
            menu_option.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
            weapon.SetInhUse(true);
        }
        else
            Debug.Log("Weapons selected already");
    }
    public void DiscoverWeapon()
    {
        weapon.Discover();
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (weapon != null)
        {
            if (weapon.GetDisplayImage() != null)
            {
                img.sprite = weapon.GetDisplayImage();
            }
            if(weapon.InUse==true)
                menu_option.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
        }
    }
}
