using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Battle;
using Movement;
using Player;
using Spells;
using Tobii.Research.Unity.Examples;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public static PlayerManager Instance;

    public PlayerMovement playerMovement;

    public event Action OnEncountered;
    //Base stats
    public float HP;
    public int defense;
    public int attack_speed;
    //skills
    public bool learn_spell_skill = false;//after finding a magic book you can learn spells
    
    //mouse, keyboard
    //[SerializeField] public List<Control> controls;
    //public InputAction mouse;
    //public InputAction keyboard;
    //public InputAction tobii;
    public TobiiControl mTobiiControl;
    
    public Slider menu_open; // Assign in Inspector
    public GameObject player;
   
    private Rigidbody2D rb;
    public float speed=0.1f;
    Animator animator;
    [SerializeField] Hp_Bar_Animation HP_player_a;
    //weapons, objects and spells
    public List<Weapons> weapons=new List<Weapons>();
    public List<Item> objects=new List<Item>();
    public List<Spell> _spells=new List<Spell>();
    //animation parameters, menu ...
    public bool _isFight = false;

    public bool IsFight
    {
        get { return _isFight; }
        set
        {
            _isFight = value;
            _isMoving=false;
        }
    }
    public bool _isMoving=true;
    public bool IsMoving { 
        get 
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            if (animator != null)
            {
                animator.SetBool("isMoving", value);
            }
            else
            {
                Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
            }
        }
    }
    public bool isHover = false;
    public bool IsMenu=false;
    public bool _eating = false;
    public bool eating
    {
        get
        {
            return _eating;
        }
        set
        {
            if (animator != null)
            {
                animator.SetBool("is_eating",value);
                animator.SetBool("isMoving",!value);
                
            }
            else
            {
                Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
            }
            _eating=value;
        }
    }

    public bool _new_item = false;
    public bool new_item
    {
        get
        {
            return _new_item;
            
        }
        private set
        {
            
            
            if (animator != null)
            {
                animator.SetBool("new_item",value);
                animator.SetBool("isMoving",!value);
                
            }
            else
            {
                Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
            }
            _new_item=value;
        }}
    public bool _isFacingRight=true;
    public bool isFacingRight { 
        get { 
                return _isFacingRight;
        } 
        private set {
            if (_isFacingRight != value)
            {
                if (animator != null)
                {
                    animator.SetBool("isFacingRight", value);
                }
                else
                {
                    Debug.LogError("Animator is missing! Make sure the Animator component is attached to the Player.");
                }

            }
            _isFacingRight = value;
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
        animator = GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();

        if (player == null)
        {
            Debug.LogError("player not found");
        }
        else
        {
            Debug.Log("player found");
        }
        if (menu_open != null)
        {
            menu_open.gameObject.SetActive(false);

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
            //push player cand atinge ca nicodata sa nu treaca de start

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
                add_item(other.gameObject.GetComponent<Item>());
                
            }
            //open box/something with the objects

        }
        if (other.gameObject.CompareTag("Magic"))
        {
            if (MagicTree.Instance.discovered != true)
            {
                Debug.Log("Learning spell");
                //animatie search tree
                new_item = true;
                Invoke("finding_animation", 2f);
                MagicTree.Instance.search_tree();
                
            }
        }
        if (other.gameObject.CompareTag("Weapons"))
        {

            Debug.Log("Weapon!!!");
            Weapons weapon = other.gameObject.GetComponent<Weapons>();
            if (weapon != null)
            {
                Debug.Log("Finding weapon");
                IsMoving = false;
                FindWeapon(weapon);
               
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
    public void start_battle(GameObject enemy)
    {
        Destroy(enemy);
        OnEncountered();
      
    }
    //Items
    private void finding_animation()
    {
        Debug.Log("Animation started");
        
        new_item = false;
        //IsMoving = true;
        
    }
    //spells
    public void learn_spell(Spell s)
    {
        if (s != null)
        {
            this._spells.Add(s.GetComponent<Spell>()); 
            s.level_up(this);
        }
    }
    public void add_item(Item i)
    {
        int index=objects.FindIndex(item => item.name.Equals(i.name));
        Debug.Log("Index is " + index);
        if (index==-1)
        {
            if (!i.finded)
            {
                Debug.Log("Adding" + i.name);
                //Invoke("finding_animation",2f);
                objects.Add(i);
                i.find_first_item();
                i.GetComponent<SpriteRenderer>().color = Color.clear;
            }
        }
        else
        {
            if (!i.finded)
            {
                objects[index].find_item();
                i.finded = true;
                i.GetComponent<SpriteRenderer>().color = Color.clear;
            }
        }
    }
    
    //Weapons
    public Weapons attack()
    {
        if (weapons.Count == 0)
        {
            return null;
        }
        else
        {
            foreach (var w in weapons)
            {
                if (w.IsInUse())
                {
                    Debug.Log("weapon is " + w.name + " damage is " + w.Damage);
                    return w;
                }
            }

        }

        return null;
    }
    //to update the HP

    public void defense_battle(int enemy_attack)
    {
        HP -= enemy_attack + (int)(defense * 0.1);
        HP_player_a.damaging_animation();
    }
    public void SelectWeapon(Weapons w)
    {
        foreach (var weapon in weapons)
        {
            weapon.SetInhUse(false);
        }
        w.SetInhUse(true);
    }
    private void LoadWeapons()
    {
        Weapons w = GameObject.Find("Sword").ConvertTo<Weapons>();
        if (w.IsDiscovered()==true)
            weapons.Add(w);
        //weapons.Add(new Weapons("Axe", "A heavy axe to dig the dirt", 60f, "Sprites/Menu/Axe"));
    }
    
    public void FindWeapon(Weapons w)
    {
        //de implementat cand collide cu o arma
        if (w.IsDiscovered() == false)
        {
            Debug.Log("arma descoperita");
            new_item = true;
            Invoke("finding_animation",2f);
            w.Discover();
            weapons.Add(w);
            SelectWeapon(w);
            w.GetComponent<SpriteRenderer>().color = Color.clear;
            Debug.Log("Discovered weapon: " + w.name);
           
            

        }

    }

    //Open Menu stuff
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(playerMovement.current_control.get_action().name);
        if (!playerMovement.current_control.get_action().name.Equals("KeyboardMove")&&!eventData.pointerEnter.name.Equals("HP_bar"))
        {
            isHover = true;
            menu_slider_open();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // Debug.Log("Slider is"+eventData.pointerEnter.name);
        if (!playerMovement.current_control.get_action().name.Equals("KeyboardMove")&&!eventData.pointerEnter.name.Equals("HP_bar"))
        {
            isHover = false;
            menu_slider_close();
        }
    }

    public void menu_slider_open()
    {
        IsMoving = false;
        //isHover = true;
        if (menu_open != null)
        {
            
            menu_open.gameObject.SetActive(true);
           
        }    
    }

    public void menu_slider_close()
    {
        //isHover = false;
        if (menu_open != null)
        {
            
            menu_open.gameObject.SetActive(false);
           
            
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
            playerMovement.current_control.Move(this);
        }
        else
        {
            Debug.Log("not moving, playermovement is null");
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

    public void setFacingDirection(float distance)
    {
        if (distance >0&&!isFacingRight)
        {
            isFacingRight = true;
        }
        else
        {
            if (distance <0&& isFacingRight)
            {
                isFacingRight = false;
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
