using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTree : MonoBehaviour
{
    public static MagicTree Instance;
    public GameObject inside_spell;
    public bool discovered;
    
    public void search_tree()
    {
        if (discovered == false)
        {
            discovered = true;
            if (inside_spell.tag == "Spells")
            {
                Spell spell = inside_spell.gameObject.GetComponent<Spell>();
                if (spell != null)
                {
                    //Debug.Log("Learning spell");
                    inside_spell.SetActive(true);
                    PlayerManager.Instance.learn_spell(spell);
                }
            }
            //pentru alte obiecte
        }
    }

    public void Awake()
    {
        Instance=this;
        if (discovered == true)
        {
            inside_spell.SetActive(true);
            //this.GetComponent<SpriteRenderer>().sprite =sprite;
            return;
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
