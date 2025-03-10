using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
public class Weapons : MonoBehaviour
{
    public GameObject weapon;
    public static Weapons Instance;
        private string description;
        public bool InUse;
        public bool Discovered;
        private float damage;
        public Sprite image;

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