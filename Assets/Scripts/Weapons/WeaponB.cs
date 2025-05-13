using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create new weapon")]
    public class WeaponB:ScriptableObject
    {
        [SerializeField]private string weaponName;
        [SerializeField]private  string description;
        [SerializeField]private  bool inUse;
        [SerializeField]private  bool discovered;
        [SerializeField]private  float damage;
        [SerializeField]private Sprite image;

        public string WeaponName => weaponName;
        public string Description => description;

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
    }
}