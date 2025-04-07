
using UnityEngine;
public class Weapons : MonoBehaviour
{
    public GameObject weapon;
    public static Weapons Instance;
    public Rigidbody2D rb;
        private string description;
        public bool InUse;
        public bool Discovered;
        public float damage;
        public Sprite image;

        public float Damage{get{return damage;}set{damage=value;}}
       //public static Sprite defaultImage;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
            return;
        }
        rb=GetComponent<Rigidbody2D>();  
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

    public Weapons(string name, string d, float da, string imagePath)
        {
            this.name = name;
            description = d;
            InUse = false;
            Discovered = false;
            damage = da;
            image=Resources.Load<Sprite>(imagePath);
        


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
        return Discovered ? image : null;
        }
   
}