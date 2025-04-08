using System;
using System.Collections.Generic;
using Battle;
using Inventory;
using Tobii.Research.Unity.Examples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static PlayerManager Instance;
        private static readonly int Moving = Animator.StringToHash("isMoving");
        private static readonly int IsEating = Animator.StringToHash("is_eating");
        private static readonly int Item = Animator.StringToHash("new_item");
        private static readonly int FacingRight = Animator.StringToHash("isFacingRight");

        public PlayerMovement playerMovement;

        public event Action OnEncountered;
        //Base stats
        [FormerlySerializedAs("HP")] public float hp;
        public int defense;
        [FormerlySerializedAs("attack_speed")] public int attackSpeed;
        //skills
        [FormerlySerializedAs("learn_spell_skill")] public bool learnSpellSkill;//after finding a magic book you can learn spells
    
        //mouse, keyboard
        //[SerializeField] public List<Control> controls;
        //public InputAction mouse;
        //public InputAction keyboard;
        //public InputAction tobii;
        public TobiiControl mTobiiControl;
    
        [FormerlySerializedAs("menu_open")] public Slider menuOpen; // Assign in Inspector
        public GameObject player;

        public float speed=0.1f;
        private Animator _animator;
        [FormerlySerializedAs("HP_player_a")] [SerializeField]
        private HpBarAnimation hpPlayerA;
        //weapons, objects and spells
        public List<WeaponBase> Weapons=new List<WeaponBase>();
        [FormerlySerializedAs("Objects")] public List<ItemObject> objects=new List<ItemObject>();
        [FormerlySerializedAs("_spells")] public List<Spell> spells=new List<Spell>();
        //animation parameters, menu ...
        [FormerlySerializedAs("_isFight")] public bool isFight;

        public bool IsFight
        {
            get => isFight;
            set
            {
                isFight = value;
                isMoving=false;
            }
        }
        [FormerlySerializedAs("_isMoving")] public bool isMoving=true;
        public bool IsMoving { 
            get => isMoving;
            set
            {
                isMoving = value;
                if (_animator != null)
                {
                    _animator.SetBool(Moving, value);
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
            }
        }
        public bool isHover;
        [FormerlySerializedAs("IsMenu")] public bool isMenu;
        [FormerlySerializedAs("_eating")] public bool isEating;
        public bool EatingAnim
        {
            get => isEating;
            set
            {
                if (_animator != null)
                {
                    _animator.SetBool(IsEating,value);
                    _animator.SetBool(Moving,!value);
                
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
                isEating=value;
            }
        }

        [FormerlySerializedAs("_new_item")] public bool newItem;
        public bool NewItem
        {
            get => newItem;
            private set
            {
            
            
                if (_animator != null)
                {
                    _animator.SetBool(Item,value);
                    _animator.SetBool(Moving,!value);
                
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }
                newItem=value;
            }}
        [FormerlySerializedAs("_isFacingRight")] public bool isFacingRightAnim=true;

        private bool IsFacingRight { 
            get => isFacingRightAnim;
            set {
                if (isFacingRightAnim != value)
                {
                    if (_animator != null)
                    {
                        _animator.SetBool(FacingRight, value);
                    }
                    else
                    {
                        Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                    }

                }
                isFacingRightAnim = value;
            } 
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }

            player = GameObject.Find("Player");
            playerMovement=FindObjectOfType<PlayerMovement>();
            _animator = GetComponent<Animator>();
            GetComponent<Rigidbody2D>();

            if (player == null)
            {
                Debug.LogError("player not found");
            }
            else
            {
                Debug.Log("player found");
            }
            if (menuOpen != null)
            {
                menuOpen.gameObject.SetActive(false);

            }
            else
            {
                Debug.LogError("Slider is not assigned in Inspector!");
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Start"))
            {
                IsMoving = true;

            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Triggered with: " + other.gameObject.name);
            if (other.gameObject.CompareTag("Start"))
            {
                IsMoving = false;
                // while_start_rb(other);
                //push player cand atinge ca niciodata sa nu treaca de start

            }
            if (other.gameObject.CompareTag("Objects"))
            {

                Debug.Log("Magic object!!!");
          
                if (other.gameObject.name.Equals("Box"))
                {
                    Debug.Log($"Object is {other.gameObject.name}");
                    //IsMoving = false;
                    if (Box.Instance.opened == false)
                    {
                        //IsMoving = false;
                    
                        Box.Instance.open_box();
                    }

                }

                else{
                
                    Debug.Log($"Object is {other.gameObject.name}");
                    add_item(other.gameObject.GetComponent<ItemObject>());
                
                }
                //open box/something with the objects

            }
            if (other.gameObject.CompareTag("Magic"))
            {
                if (MagicTree.Instance.discovered != true)
                {
                    Debug.Log("Learning spell");
                    //animatie search tree
                    NewItem = true;
                    Invoke(nameof(finding_animation), 2f);
                    MagicTree.Instance.search_tree();
                
                }
            }
            if (other.gameObject.CompareTag("Weapons"))
            {

                Debug.Log("Weapon!!!");
                WeaponBase weaponItem = other.gameObject.GetComponent<WeaponBase>();
                if (weaponItem != null)
                {
                    Debug.Log("Finding weapon");
                    IsMoving = false;
                    FindWeapon(weaponItem);
               
                }

            }
            //declansare battle
            if (other.gameObject.CompareTag("Enemy"))
            {
                IsMoving = false;
                start_battle(other.gameObject);
            }

       
        }
        //start battle
        private void start_battle(GameObject enemy)
        {
            Destroy(enemy);
            OnEncountered?.Invoke();
        }
        //Items
        private void finding_animation()
        {
            Debug.Log("Animation started");
        
            NewItem = false;
            //IsMoving = true;
        
        }
        //spells
        public void learn_spell(Spell s)
        {
            if (s != null)
            {
                this.spells.Add(s.GetComponent<Spell>()); 
                s.level_up(this);
            }
        }

        private void add_item(ItemObject i)
        {
            int index=objects.FindIndex(item => item.itemName.Equals(i.itemName));
            Debug.Log("Index is " + index);
            if (index==-1)
            {
                if (!i.found)
                {
                    Debug.Log("Adding" + i.itemName);
                    //Invoke("finding_animation",2f);
                    objects.Add(i);
                    i.find_first_item();
                    i.GetComponent<SpriteRenderer>().color = Color.clear;
                }
            }
            else
            {
                if (!i.found)
                {
                    objects[index].find_item();
                    i.found = true;
                    i.GetComponent<SpriteRenderer>().color = Color.clear;
                }
            }
        }
    
        //Weapons
        public WeaponBase Attack()
        {
            if (Weapons.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var w in Weapons)
                {
                    if (w.IsInUse())
                    {
                        Debug.Log("weapon is " + w.WeaponName + " damage is " + w.Damage);
                        return w;
                    }
                }

            }

            return null;
        }
        //to update the HP

        public void defense_battle(int enemyAttack)
        {
            hp -= enemyAttack + (int)(defense * 0.1);
            hpPlayerA.damaging_animation();
        }

        private void SelectWeapon(WeaponBase w)
        {
            foreach (var weapon in Weapons)
            {
                weapon.SetInhUse(false);
            }
            w.SetInhUse(true);
        }

        public void FindWeapon(WeaponBase w)
        {
            //de implementat cand collide cu o arma
            if (w.IsDiscovered() == false)
            {
                Debug.Log("arma descoperita");
                NewItem = true;
                Invoke(nameof(finding_animation),2f);
                w.Discover();
                Weapons.Add(w);
                SelectWeapon(w);
                w.Weapon.GetComponent<SpriteRenderer>().color = Color.clear;
                Debug.Log("Discovered weapon: " + w.WeaponName);
           
            

            }

        }

        //Open Menu stuff
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(playerMovement.CurrentControl.get_action().name);
            if (!playerMovement.CurrentControl.get_action().name.Equals("KeyboardMove")&&!eventData.pointerEnter.name.Equals("HP_bar"))
            {
                isHover = true;
                menu_slider_open();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log("Slider is"+eventData.pointerEnter.name);
            if (!playerMovement.CurrentControl.get_action().name.Equals("KeyboardMove")&&!eventData.pointerEnter.name.Equals("HP_bar"))
            {
                isHover = false;
                menu_slider_close();
            }
        }

        public void menu_slider_open()
        {
            IsMoving = false;
            //isHover = true;
            if (menuOpen != null)
            {
            
                menuOpen.gameObject.SetActive(true);
           
            }    
        }

        public void menu_slider_close()
        {
            //isHover = false;
            if (menuOpen != null)
            {
            
                menuOpen.gameObject.SetActive(false);
           
            
            }
            IsMoving = true;
        }

        public void HandleUpdate()
        {
            if (player == null || isHover)
            {
                return;

            }
        
            if (playerMovement != null)
            {
                // Debug.Log(playerMovement.current_control);
                playerMovement.CurrentControl.Move(this);
            }
            else
            {
                Debug.Log("not moving, player movement is null");
            }
            //     UnityEngine.Vector3 mousePos = Input.mousePosition;
            //
            //     mousePos.z = Camera.main.nearClipPlane;
            //     UnityEngine.Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
            //     //restrict moving only on the x axis: player.transform.position.y
            //
            //     UnityEngine.Vector3 mouseNext =
            //         new UnityEngine.Vector3(worldMouse.x, player.transform.position.y, worldMouse.z);
            //     if (MenuManager.Instance.current_menu.activeSelf == false)
            //     {
            //
            //         float distance = mouseNext.x - player.transform.position.x;
            //         setFacingDirection(distance);
            //         RaycastHit2D hit = Physics2D.Raycast(mousePos, UnityEngine.Vector2.zero);
            //         if ((hit.collider != null && hit.collider.gameObject == player)||new_item==true)
            //
            // {
            //                IsMoving = false;
            //                 return;
            //             }
            //             else
            //             {
            //                 if (player.transform.position.x < 398)
            //                 {
            //                     //IsMoving = false;
            //                     player.transform.position = UnityEngine.Vector3.MoveTowards(player.transform.position,
            //                         new Vector2(400f, player.transform.position.y), Time.deltaTime * 50f);
            //                 }
            //                 else
            //                 {
            //
            //                     IsMoving = true;
            //                     player.transform.position =
            //                         UnityEngine.Vector3.MoveTowards(player.transform.position, mouseNext, Time.deltaTime * 50f);
            //                 }
            //             }
            //         
            //     }
            //
            //         
            //     else
            //       {
            //         IsMoving = false;
            //         return;
            //             //rb.velocity = UnityEngine.Vector3.zero;
            //       }
            //     
            //     //CheckIfPlayerArrived();
        }

        public void SetFacingDirection(float distance)
        {
            if (distance >0&&!IsFacingRight)
            {
                IsFacingRight = true;
            }
            else
            {
                if (distance <0&& IsFacingRight)
                {
                    IsFacingRight = false;
                }

            }
        }
        /*private void CheckIfPlayerArrived()
{

   Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, player.transform.position.z - Camera.main.transform.position.z));
   float playerWidth = player.GetComponent<Renderer>().bounds.extents.x; // Get half-width of player

   // If mouse is within the player's width, trigger OnPointerEnter manually
   if (Mathf.Abs(mouseWorldPos.x - player.transform.position.x) < playerWidth)
   {
       if (!isHover) // If not already hovering, call OnPointerEnter
       {
           PointerEventData pointerData = new PointerEventData(EventSystem.current);
           OnPointerEnter(pointerData);
       }
   }
   else
   {
       if (isHover) // If hovering and now outside, call OnPointerExit
       {
           PointerEventData pointerData = new PointerEventData(EventSystem.current);
           OnPointerExit(pointerData);
       }
   }
}*/
    }
}