using System;
using System.Collections;
using System.Collections.Generic;
using Spells;
using UnityEngine;

public class MagicTree : MonoBehaviour
{
    public static MagicTree Instance;
    [SerializeField] Spell  inside_spell;
    public bool discovered;
    
    public void search_tree()
    {
        if (discovered == false)
        {
            discovered = true;
               
                if(inside_spell != null)
                    Debug.Log(inside_spell.name);
                else
                {
                    Debug.Log("spell not loaded");
                }
                if (inside_spell != null)
                {
                    //Debug.Log("Learning spell");
                    //spell.SetActive(true);
                    PlayerManager.Instance.learn_spell(inside_spell);
                }
            //pentru alte obiecte
        }
    }

    public void Awake()
    {
        Instance=this;
        if (discovered == true)
        {
           // inside_spell.SetActive(true);
           //inside_spell=Resources.Load<SpellBase>("");
            //this.GetComponent<SpriteRenderer>().sprite =sprite;
            return;
        }
    }
}
