using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Enemies;
using Inventory;
using TMPro;
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
        //pentru notifications
        [SerializeField]private TextMeshProUGUI notification;
        private static readonly int Moving = Animator.StringToHash("isMoving");
        private static readonly int Eating = Animator.StringToHash("is_eating");
        private static readonly int Item = Animator.StringToHash("new_item");
        private static readonly int FacingRight = Animator.StringToHash("isFacingRight");

        public PlayerMovement playerMovement;

        public event Action<Enemy> OnEncountered;
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

        public HpBarAnimation HpPlayerA
        {
            get => hpPlayerA;
            set => hpPlayerA = value;
        }
        //weapons, objects and spells
        [SerializeField]private InventoryManager inventory;
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
        [FormerlySerializedAs("_eating")] private bool isEating;
        public bool IsEating
        {
            get => isEating;
            set
            {
                if (_animator != null)
                {
                    _animator.SetBool(Eating,value);
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
            set
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
           // notification = GetComponent<TextMeshProUGUI>();
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
          
                if (other.gameObject.name.Equals("Box"))
                {
                    Debug.Log($"Object is {other.gameObject.name}\n");
                    //IsMoving = false;
                    if (Box.Instance.opened == false)
                    {
                        //IsMoving = false;
                        notification.text = "";
                        StartCoroutine(notification_show("Opening Treasure Chest\n"));
                        IsMoving = false;
                        var b =other.GetComponent<Box>();
                        b.open_box();
                        //Box.Instance.open_box();
                    }

                }

                else{
                   // Debug.Log($"Object is {other.gameObject.name}");
                    inventory.add_food(other.gameObject);
                }
                //open box/something with the objects

            }
            if (other.gameObject.CompareTag("Magic"))
            {
                if (MagicTree.Instance.discovered != true)
                {
                    notification.text = "";
                    StartCoroutine(notification_show("You found a magic tree...\n"));
                   // Debug.Log("Learning spell");
                    //animatie search tree
                    NewItem = true;
                    Invoke(nameof(finding_animation), 2f);
                    var m_tree = other.GetComponent<MagicTree>();
                    m_tree.search_tree();
                
                }
            }
            if (other.gameObject.CompareTag("Weapons"))
            {
               
               // Debug.Log("Weapon!!!");
                WeaponBase weaponItem = other.gameObject.GetComponent<WeaponBase>();
                if (weaponItem != null)
                {
                    //Debug.Log("Finding weapon");
                    IsMoving = false;
                    InventoryManager.Instance.FindWeapon(other.gameObject);
               
                }

            }
            //declansare battle
            if (other.gameObject.CompareTag("Enemy"))
            {
                IsMoving = false;
                start_battle(other.gameObject);
            }

       
        }
        public IEnumerator notification_show(string final_text)
        {
            notification.text = "";
            foreach (var letter in final_text.ToCharArray())
            {
                if (letter.Equals('\n'))
                {
                    yield return new WaitForSeconds(1f/20);
                }
                notification.text += letter;
                yield return new WaitForSeconds(1f/30);
            }
            
            // new WaitForSeconds(2f);
            Invoke(nameof(notification_delete),1f);


        }
        

        private void notification_delete()
        {
            notification.text = "";
        }
        
        //start battle
        private void start_battle(GameObject enemy)
        {
            StartCoroutine(
                notification_show($"You encountered an enemy \n {enemy.GetComponent<Enemy>().EnemieBase.name}"));
            OnEncountered?.Invoke(enemy.GetComponent<Enemy>());
            Destroy(enemy);
        }
        //Items
        public void finding_animation()
        {
            Debug.Log("Animation started");
        
            NewItem = false;
            //IsMoving = true;
        
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
    }
}