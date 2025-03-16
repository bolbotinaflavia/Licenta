using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static Box Instance;
    public GameObject inside_object;
    public Sprite sprite;
    public bool opened;
    public GameObject player;
    //de vazut cu animatia;
    //cometariii

    public void open_box()
    {
        if (opened == false)
        {
            opened = true;
            this.GetComponent<SpriteRenderer>().sprite = sprite;
            Update();
            if (inside_object.tag == "Weapons")
            {
                Weapons weapon = inside_object.gameObject.GetComponent<Weapons>();
                if (weapon != null)
                {
                    Debug.Log("Finding weapon");
                    inside_object.SetActive(true);
                    PlayerManager.Instance.FindWeapon(weapon);
                    
                }
            }
            //pentru alte obiecte
        }
    }
    
    private void Awake()
    {
        Instance=this;
        if (opened == true)
        {
            inside_object.SetActive(true);
            this.GetComponent<SpriteRenderer>().sprite =sprite;
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(inside_object.activeSelf)
        {
            
            inside_object.GetComponent<SpriteRenderer>().color=Color.clear;
           // inside_object.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (opened == true)
        {
            inside_object.SetActive(true);
            this.GetComponent<SpriteRenderer>().sprite =sprite;
        }
    }
}
