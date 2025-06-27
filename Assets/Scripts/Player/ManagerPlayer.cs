using System;
using System.Collections;
using Animations;
using DefaultNamespace;
using Enemies;
using Inventory;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weapons;
using StaticObjects;
using UnityEngine.SceneManagement;

namespace Player
{
    [PlantUmlDiagram]
    public class PlayerManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static PlayerManager Instance;
        //pentru notifications
        [SerializeField]private Notification notification;

        public Notification Notification
        {
            get { return notification; }
        }
        private static readonly int Moving = Animator.StringToHash("isMoving");
        private static readonly int Eating = Animator.StringToHash("is_eating");
        private static readonly int Item = Animator.StringToHash("new_item");
        private static readonly int FacingRight = Animator.StringToHash("isFacingRight");
        private static readonly int BBattle=Animator.StringToHash("begin_battle");
 private bool isMoving=true;
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
        public PlayerMovement playerMovement;

        public event Action<Enemy,GameObject> OnEncountered;
        //Base stats
        [FormerlySerializedAs("HP")] public float hp;
        public int defense;
        [FormerlySerializedAs("attack_speed")] public int attackSpeed;
        //skills
        [FormerlySerializedAs("learn_spell_skill")] public bool learnSpellSkill=false;//after finding a magic book you can learn spells
    
        //mouse, keyboard
        //[SerializeField] public List<Control> controls;
        //public InputAction mouse;
        //public InputAction keyboard;
        //public InputAction tobii;
    
        [FormerlySerializedAs("menu_open")] public Slider menuOpen; // Assign in Inspector
        public GameObject player;

        public float speed=1f;
        private Animator _animator;
        public Animator Animator
        {
            get => _animator;
            set => _animator = value;
        }
        [FormerlySerializedAs("HP_player_a")] [SerializeField]
        private HpBarAnimation hpPlayerA;

        public HpBarAnimation HpPlayerA
        {
            get => hpPlayerA;
            set => hpPlayerA = value;
        }
        //weapons, objects and spells
        [SerializeField]private InventoryManager inventory;

        public InventoryManager Inventory
        {
            get => inventory;
        }
        //animation parameters, menu ...
       
        public bool isHover;
        [FormerlySerializedAs("IsMenu")] public bool isMenu;
        [SerializeField][FormerlySerializedAs("_eating")] private bool isEating;
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
                    Debug.LogError("Animator is missing");
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
                    Debug.LogError("Animator is missing.");
                }
                newItem=value;
            }}
        [FormerlySerializedAs("_isFacingRight")] public bool isFacingRightAnim=true;

        public bool IsFacingRight { 
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
                        Debug.LogError("Animator is missing.");
                    }

                }
                isFacingRightAnim = value;
            } 
        }

        public bool beginBattle=false;

        private bool BeginBattle
        {
            get => beginBattle;
            set
            {
                if (_animator != null)
                {
                    
                    _animator.SetBool(BBattle, value);
                    _animator.SetBool(Moving,!value);
                }
                else
                {
                    Debug.LogError("Animator is missing");
                }

                beginBattle = value;
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
            playerMovement = FindObjectOfType<PlayerMovement>();
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
                menuOpen.GetComponent<CanvasGroup>().alpha = 1f;
                menuOpen.gameObject.SetActive(false);

            }
            else
            {
                Debug.LogError("Slider is not assigned in Inspector!");
            }
            IsFacingRight = true;
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

            if (other.gameObject.CompareTag("Finish"))
            {
                StartCoroutine(Notification.notification_show("You found the sword you were looking for!!", 2f));
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("END"))
            {
                IsMoving = false;
                SceneManager.LoadScene("HappyEnding");
            }
            if (other.gameObject.CompareTag("Objects"))
            {
          
                if (other.gameObject.name.Equals("Box"))
                {
                    IsMoving = false;
                    if (other.gameObject.GetComponent<Box>().opened == false)
                    {
                        NewItem = true;
                        var b =other.GetComponent<Box>();
                        b.open_box();
                    }
                    else
                    {
                        IsMoving = true;
                    }

                }

                else{
                    inventory.add_food(other.gameObject);
                }

            }
            if (other.gameObject.CompareTag("Magic"))
            {
                IsMoving = false;
                if (other.gameObject.GetComponent<MagicTree>().discovered != true)
                {
                    notification.Message.text = "";
                  //  StartCoroutine(notification.notification_show("You found a magic tree...\n",2f));
                    NewItem = true;
                    var m_tree = other.GetComponent<MagicTree>();
                    m_tree.search_tree();
                
                }
            }
            if (other.gameObject.CompareTag("Weapons"))
            {
                IsMoving = false;
               // Debug.Log("Weapon!!!");
                Weapon weaponItem = other.gameObject.GetComponent<Weapon>();
                if (weaponItem != null)
                {
                    //Debug.Log("Finding weapon");
                    IsMoving = false;
                    NewItem = true;
                    InventoryManager.Instance.FindWeapon(other.gameObject);
               
                }

            }

            if (other.gameObject.CompareTag("Door"))
            {
                EventClass level2 = new EventClass
                {
                    FabulousString="level 2 loading",
                    SparklingInt= 1,
                    SpectacularFloat=1.0f,
                    PeculiarBool= true
                };
                UnityServices.InitializeAsync();
                AnalyticsService.Instance.RecordEvent(level2);
                Door.Instance.Open_Door();
                if(Door.Instance.Opened)
                    Door.Instance.gameObject.GetComponent<Collider2D>().enabled = false;
            }
            //declansare battle
            if (other.gameObject.CompareTag("Enemy"))
            {
                IsMoving = false;
               BeginBattle = true;
               StartCoroutine(new_battle_anim());
               StartCoroutine(start_battle_a(other.gameObject));
            }

            if (other.gameObject.CompareTag("Artefacts"))
            {
                IsMoving = false;
                    NewItem = true;
                    InventoryManager.Instance.add_artefact(other.gameObject);
            }

            //IsMoving = true;
           menuOpen.gameObject.SetActive(false);
        }

        private IEnumerator new_battle_anim()
        {
            yield return new WaitForSeconds(3f);
            BeginBattle = false;
            IsMoving = true;
        }

        private IEnumerator start_battle_a(GameObject enemy)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(start_battle(enemy));
            
        }
        //start battle
        public IEnumerator start_battle(GameObject enemy)
        {
           menuOpen.gameObject.SetActive(false);
            StartCoroutine(
                notification.notification_show($"You encountered an enemy \n {enemy.GetComponent<Enemy>().EnemieBase.name}",1f));
            OnEncountered?.Invoke(enemy.GetComponent<Enemy>(),enemy);
            yield return new WaitForSeconds(2f);
        }
        //Items
        public void finding_animation()
        {
            Debug.Log("Animation started");
            NewItem = false;
            IsMoving = true;
        
        }

        //Open Menu stuff
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!playerMovement.CurrentControl.get_action().name.Equals("KeyboardMove")&&!eventData.pointerEnter.name.Equals("HP_bar"))
            {
                isHover = true;
                menu_slider_open();
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!playerMovement.CurrentControl.get_action().name.Equals("KeyboardMove")&&!eventData.pointerEnter.name.Equals("HP_bar"))
            {
                isHover = false;
                menu_slider_close();
            }
        }
        public void menu_slider_open()
        {
            IsMoving = false;
            isHover = true;
            if (menuOpen != null)
            {
                menuOpen.gameObject.SetActive(true);
            }    
        }
        public void menu_slider_close()
        {
            isHover = false;
            if (menuOpen != null)
            {
               menuOpen.gameObject.SetActive(false);
            }
            IsMoving = true;
        }

        public void HandleUpdate()
        {
            if (player == null)
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