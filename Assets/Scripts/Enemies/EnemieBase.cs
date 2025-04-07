using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Enemies", menuName = "Enemies/Create new enemy")]
public class EnemieBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private Sprite sprite1;
    //aici ar trebui animatii
    [SerializeField] private Sprite attack_s;
    [SerializeField] private Sprite defense_s;
    [SerializeField] private Weapon_weakness w1;
    [SerializeField] private Spell_weakness w2;
    
    //Base stats
    
    [SerializeField] private float hp_max;
    [SerializeField] private int attack;
    [SerializeField] private float defense;
    [SerializeField] private int speed;

    public List<string> get_all_enemies_entities()
    {
        List<string> all_enemies = new List<string>();
        all_enemies= GameObject.FindObjectsOfType<EnemieBase>().ToList().ConvertAll(x => x.name);
        return all_enemies;
    }

    public float Hp_max { get => hp_max; set => hp_max = value; }

    public int Attack
    {
        get { return attack; }
    }

    public float Defense
    {
        get { return defense; }
    }

    public int Speed
    {
        get { return speed; }
    }
    public Weapon_weakness W1 { get => w1; set => w1 = value; }
    public Spell_weakness W2 { get => w2; set => w2 = value; }
}

public enum Weapon_weakness
{
    //all specials are +5 damage depending on the type
    Sword, //general use
    Crossbow, //good for flighting enemies(ghosts)
    Trowel, //close contact enemies(zombies)
    Shovel // good for vampires, skeletons
    
    
}

public enum Spell_weakness
{
//names 
    Brisingr,//fire spell
    Jierda,//cracking bones
    Slytha, //sleep spell
    Holy_Blaze,//blinding light
    
}