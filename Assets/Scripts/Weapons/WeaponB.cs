using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create new weapon")]
    public class WeaponB : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeField] private string description;
        [SerializeField] private bool inUse;
        [SerializeField] private bool discovered;
        [SerializeField] private float damage;
        [SerializeField] private Sprite image;

        public string WeaponName
        {
            get => weaponName;
            set => weaponName = value;
        }
        public string Description {
            get => description;
            set => description = value;
        }

        public bool InUse
        {
            get => inUse;
            set => inUse = value;
        }

        public bool Discovered
        {
            get => discovered;
            set => discovered = value;
        }
        public float Damage => damage;
        public Sprite Image => image;

        public Sprite GetDisplayImage()
        {
            return Discovered ? Image : null;
        }
        public WeaponB(string name, string desc, bool use, bool disc, float dmg, Sprite img)
        {
            weaponName = name;
            description = desc;
            inUse = use;
            discovered = disc;
            damage = dmg;
            image = img;
        }
    }
}