using System;
using System.Collections.Generic;
using System.Linq;
using Battle;
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
        [SerializeField] private List<ArtefactBase> artefacts=new List<ArtefactBase>();

        //public InventoryManager Instance => _instance;
        public List<global::Weapons.WeaponB> Weapons => weapons;
        public WeaponB CurrentWeapon => currentWeapon;
        public List<FoodBase> Food => food;
        public List<ItemObject> Items => items;
        public List<Spells.Spell> Spells => spells;
        public List<ArtefactBase> Artefacts => artefacts;

        public void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void add_object(GameObject obj){
            if (obj.tag.Equals("Spells"))
            {
                learn_spell(obj.GetComponent<Spells.Spell>());
            }
            else
            {
                if (obj.tag.Equals("Artefacts"))
                {
                    add_artefact(obj);
                }
                else
                {
                    if (obj.tag.Equals("Weapons"))
                    {
                        FindWeapon(obj);
                    }
                    else
                    {
                        if (obj.tag.Equals("Food"))
                        {
                            add_food(obj);
                        }
                    }
                }
            }
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
                StartCoroutine(PlayerManager.Instance.Notification.notification_show("You found a new weapon\n Check your bag...",2f));
                PlayerManager.Instance.menuOpen.GetComponent<CanvasGroup>().alpha = 0f;
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
                if (PlayerManager.Instance.learnSpellSkill)
                {
                    if (spells.Contains(s))
                    {
                        spells.Find(spell => spell.SpellBase.Equals(s.SpellBase)).level_up();
                    }
                    else
                    {
                        spells.Add(s.GetComponent<Spells.Spell>());
                        spells.Last().level_up();
                    }

                    StartCoroutine(PlayerManager.Instance.Notification.notification_show("Learning a spell...\n",2f));
                }
                else
                {
                    StartCoroutine(PlayerManager.Instance.Notification.notification_show("You cannot learn the spell yet\n",2f));
                }
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
            StartCoroutine(PlayerManager.Instance.Notification.notification_show($"You found a {new_food.FoodName}",2f));
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
        
        //artefacts
        public void add_artefact(GameObject a)
        {
            var artefact =
                Resources.Load<ArtefactBase>($"Artefacts/{a.GetComponent<Artefact>().ArtefactBase.ArtefactName}");
            if (artefacts.Count+1 > 4)
            {
                PlayerManager.Instance.Notification.notification_show("You have too many artefacts!",2f);
            }
            else
            {
                StartCoroutine(PlayerManager.Instance.Notification.notification_show("You found a new artefact\n Check your bag...",2f));
                PlayerManager.Instance.menuOpen.GetComponent<CanvasGroup>().alpha = 0f;
                PlayerManager.Instance.NewItem = true;
                Invoke(nameof(finding_animation),2f);
                artefacts.Add(artefact);
                if (artefact.ArtefactName.Equals("SpellBook"))
                    PlayerManager.Instance.learnSpellSkill = true;
                new WaitForSeconds(5f);
                Destroy(a);
            }
        }

        public ArtefactBase getArtefact(string name)
        {
            foreach (var a in artefacts)
            {
                if (a.ArtefactName == name)
                    return a;
            }
            return null;
        }
        
        //animations
        public void finding_animation()
        {
            Debug.Log("Animation started");
            PlayerManager.Instance.NewItem = false;
            PlayerManager.Instance.menuOpen.GetComponent<CanvasGroup>().alpha = 1f;
            PlayerManager.Instance.menuOpen.gameObject.SetActive(false);
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