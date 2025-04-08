using UnityEngine;
using UnityEngine.Serialization;

namespace Weapon_scriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create new weapon")]
    public class Weapon:ScriptableObject
    {
        [FormerlySerializedAs("name")] [SerializeField] private string weaponName;
        [SerializeField] private Sprite icon;
        [SerializeField] private int damage;
        [FormerlySerializedAs("InUse")] [SerializeField] private bool inUse;
        [FormerlySerializedAs("Discovered")] [SerializeField] private bool discovered;
        [FormerlySerializedAs("_specialAttack")] [SerializeField] private SpecialAttack specialAttack;
    }
    
}

public enum SpecialAttack
{
    None,
    Skeleton,
    Ghost,
    Zombie,
    //de facut sa ia ce enemies am
}