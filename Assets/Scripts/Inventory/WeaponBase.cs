using UnityEngine;

namespace Inventory
{
    public class WeaponBase:MonoBehaviour
    {
        public GameObject Weapon;
        private static WeaponBase _instance;
        public Rigidbody2D Rb;
        public string WeaponName;
        private string _description;
        public bool InUse;
        public bool Discovered;
        public float Damage;
        public Sprite Image;

        //public static Sprite defaultImage;
   
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                //Destroy(gameObject);
                return;
            }
            Rb=Weapon.GetComponent<Rigidbody2D>();  
        }

        /* public Weapon(string name, string description, bool inUse, bool discovered, float damage, Sprite image)
         {
             this.name = name;
             this.description = description;
             InUse = inUse;
             Discovered = discovered;
             this.damage = damage;
             this.image = image;
             if (defaultImage == null)
                 defaultImage = Resources.Load<Sprite>("Sprites/Menu/QuestionMark");
         }*/

        public WeaponBase(string name, string d, float da, string imagePath)
        {
            WeaponName = name;
            _description = d;
            InUse = false;
            Discovered = false;
            Damage = da;
            Image=Resources.Load<Sprite>(imagePath);
        


        }
        /* public Weapons()
         {
             name = "Weapon";
             description = "";
             InUse = false;
             Discovered = false;
             damage = 10;
         }*/

        public void Discover()
        {
            this.Discovered = true;
        }
        public void SetInhUse(bool state)
        {
            this.InUse = state;
        }
        public bool IsInUse()
        {
            return this.InUse;
        }
        public bool IsDiscovered()
        {
            
            return this.Discovered;
        }
        public Sprite GetDisplayImage()
        {
            return Discovered ? Image : null;
        }
    }
}