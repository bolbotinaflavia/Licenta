using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Player;
using Sliders_scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Inventory
{
    public class InventoryManager:MonoBehaviour
    {
        public static InventoryManager Instance;
        [SerializeField]private List<WeaponB> weapons=new List<WeaponB>();
        [SerializeField] private WeaponB currentWeapon;
        [SerializeField] private List<FoodBase> food=new List<FoodBase>();
        [SerializeField] private List<ItemObject> items=new List<ItemObject>();
        [SerializeField] private List<Spells.Spell> spells=new List<Spells.Spell>();

        //public InventoryManager Instance => _instance;
        public List<global::Weapons.WeaponB> Weapons => weapons;
        public WeaponB CurrentWeapon => currentWeapon;
        public List<FoodBase> Food => food;
        public List<ItemObject> Items => items;
        public List<Spells.Spell> Spells => spells;

        public void Awake()
        {
            if(Instance==null) Instance = this;
            DontDestroyOnLoad(this);
        }
        //Weapons stuff
        public WeaponB Attack()
        {
            if (weapons.Count == 0)
            {
                return null;
            }
            else
            {
                return currentWeapon;
            }
        }
        public void SelectWeapon(WeaponB w)
        {
            foreach (var weapon in weapons.Where(weapon => w.WeaponName == weapon.WeaponName))
            {
                currentWeapon =  Resources.Load<WeaponB>($"Weapons/{w.WeaponName}");
            }
        }

        public bool check_weapons(string name)
        {
            foreach (var w in weapons)
            {
                if (w.WeaponName == name)
                    return true;
            }

            return false;
        }
        public void FindWeapon(GameObject w)
        {
            var new_weapon =
                Resources.Load<WeaponB>($"Weapons/{w.GetComponent<Weapon>().WeaponB.WeaponName}");
            //de implementat cand collide cu o arma
            if (check_weapons(new_weapon.WeaponName)==false)
            {
                StartCoroutine(PlayerManager.Instance.notification_show("You found a new weapon\n Check your bag..."));
                PlayerManager.Instance.NewItem = true;
                Invoke(nameof(finding_animation),2f);
                weapons.Add(new_weapon);
                SelectWeapon(new_weapon);
                PlayerManager.Instance.IsMoving = true;
                new WaitForSeconds(5f);
                Destroy(w);
            }

        }
        
        //Spell stuff
        public void learn_spell(Spells.Spell s)
        {
            if (s != null)
            { 
                spells.Add(s.GetComponent<Spells.Spell>());
                spells.Last().level_up();
                //StartCoroutine(notification_show("Learning a spell...\n"));
            }
        }
        public Spells.Spell getSpell(string name)
        {
            foreach (var s in spells)
            {
                if (s.SpellBase.SpellName == name)
                    return s;
            }
            return null;
        }
        
        //Food stuff
        public void add_food(GameObject f)
        {
            var new_food=Resources.Load<FoodBase>($"Food/{f.GetComponent<Food>().Base.FoodName}");
            food.Add(new_food);
            Destroy(f,0.2f);
        }
        public int get_number_of_food_item(string name)
        {
            return food.Count(x => x.FoodName == name);
        }
        public void Consume(FoodBase f)
        {
            if (get_number_of_food_item(f.FoodName) <= 0) return;
            PlayerManager.Instance.IsEating = true;
            HpSlider.Instance.UpdateUI();
            food.Remove(f);
            PlayerManager.Instance.HpPlayerA.healing_animation();
            Invoke(nameof(eating_animation),2f);
        }
        
        //animations
        public void finding_animation()
        {
            Debug.Log("Animation started");
        
            PlayerManager.Instance.NewItem = false;
            //IsMoving = true;
        
        }
        private void eating_animation()
        {
            new WaitForSeconds(3f);
            PlayerManager.Instance.IsEating = false;
            //IsMoving = true;
        
        }
      
    }
}