using System;
using System.Collections.Generic;
using System.Linq;
using Spells;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spell_select : Menu_countdown
{
    public Spell s;
    public string name;
    private Scene scene;
    public TextMeshProUGUI text;
    public TextMeshProUGUI description;
    public GameObject canvas_description;
    protected override void OnTimerComplete()
    {
        if (s != null)
        {
            if (scene.name.Equals("Fight"))
            {
                use_spell();
            }
            else
            {
                open_description();
                UpdateUI();
            }
            
        }
        else
        {
            Debug.Log("Spell is not available yet");
        }
        
    }

    public void open_description()
    {
        canvas_description.GameObject().SetActive(true);
        description.GameObject().SetActive(true);
    }
    public void FindSpellsInGame()
    {
        s= GameObject.Find(name).ConvertTo<Spell>();
        
    }
    public void use_spell()
    {
        //DE VAZUT LA FIGHT
    }

    public void UpdateUI()
    {
        Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);
        if (s != null)
        {
            if (s.get_magic_level()==0)
            {
                menu_option.fillRect.GetComponent<Image>().color = Color.gray;
                text.text = "-1@3~%%$@";
                description.text="No information available";
            }
            else
            {
                menu_option.fillRect.GetComponent<Image>().color = new Color(0.9568627f, 0.7058824f, 0.1058824f);
              
                if (s.get_magic_level().Equals(0))
                {
                    text.text = s.name;
                }

                if (s.get_magic_level().Equals(1))
                {
                    text.text = name;
                    description.text="No information available";
                }

                if (s.get_magic_level().Equals(2))
                {
                    text.text = name;
                    description.text = s.Description2;
                    Debug.Log(description.text);
                }

                if (s.get_magic_level().Equals(3))
                {
                    text.text = name;
                    description.text = s.Description2+"/n"+s.Description3;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        FindSpellsInGame();
        // canvas_description.GetComponent<Sprite>();
        // text = GetComponent<TextMeshProUGUI>();
        // description = GetComponent<TextMeshProUGUI>();
        if(canvas_description!=null)
            canvas_description.SetActive(false);
        if(description!=null)
             description.GameObject().SetActive(false);
        scene=SceneManager.GetActiveScene();
        UpdateUI();
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
