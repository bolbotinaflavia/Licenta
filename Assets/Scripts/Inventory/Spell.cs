using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public bool discovered = false;
    public string spell_name;

    private double effect;
    // Start is called before the first frame update
    public void discover()
    {
        discovered = true;
    }

    public bool isDiscovered()
    {
        return discovered;
    }
    private void learning_spell()
    {
        Debug.Log("Animation started");
        //de facut animatie
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
