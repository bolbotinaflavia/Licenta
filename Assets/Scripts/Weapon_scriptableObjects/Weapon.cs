using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapon_scriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create new weapon")]
    public class Weapon:ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;
        [SerializeField] private int damage;
        [SerializeField] private bool InUse;
        [SerializeField] private bool Discovered;
        [SerializeField] private Special_Attack _specialAttack;
    }
    
}

public enum Special_Attack
{
    none,
    Skeleton,
    Ghost,
    Zombie,
    //de facut sa ia ce enemies am
}