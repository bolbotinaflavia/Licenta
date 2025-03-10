using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

struct Weapon{
    
};

public class PlayerManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static PlayerManager Instance;
    private GameObject player;
    private Rigidbody2D rb;

    //running variables
    public float speed=0.1f;
    public bool _isMoving=true;
    public bool IsMoving { get 
        {
            return _isMoving;
        }
        private set
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
    Animator animator;
    //weapons -> 5
    public List<Weapons> weapons=new List<Weapons>();

    //public GameObject weaponPrefab;
    //public Transform weaponSpawnPoint;

    //spells -> 5

    private bool isHover = false;
    private bool stop_start;

    public Slider menu_open; // Assign in Inspector
    public bool IsMenu=false;
    public bool _isFacingRight=true;
    public bool isFacingRight { get { 
                return _isFacingRight;
        } private set {
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
        Instance = this;
        
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        //LoadWeapons();
        //SpawnWeapons();

        if (player == null)
        {
            Debug.LogError("player not found");
        }
        else
        {
            Debug.Log("player found");
        }
       /* if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
       */
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
            //push player cand atinge ca nicodata sa nu treaca de start

        }
        if (other.gameObject.CompareTag("Objects"))
        {

            Debug.Log("Magic object!!!");
            Debug.Log($"Object is {other.gameObject.name}");
            if (other.gameObject.name.Equals("Box"))
            {
                IsMoving = false;
                Box.Instance.open_box();
                IsMoving = true;
            }
            
            //open box/something with the objects

        }
        if (other.gameObject.CompareTag("Weapons"))
        {

            Debug.Log("Weapon!!!");
            Weapons weapon = other.gameObject.GetComponent<Weapons>();
            if (weapon != null)
            {
                Debug.Log("Finding weapon");
                FindWeapon(weapon);
            }

        }
    }
    //Objects


    //Weapons
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
            w.Discover();
            weapons.Add(w);
            SelectWeapon(w);
            w.GetComponent<SpriteRenderer>().color = Color.clear;
            Debug.Log("Discovered weapon: " + w.name);

        }

    }

  /*  private void SpawnWeapons()
    {
        foreach(Weapons weapon in weapons)
        {
            GameObject newWeapon = Instantiate(weaponPrefab,weaponSpawnPoint.position,Quaternion.identity);
            newWeapon.GetComponent<SpriteRenderer>().sprite = weapon.GetDisplayImage();
            newWeapon.GetComponent<Weapons_menu>().w = weapon;
        }
    }*/

    //Open Menu stuff
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsMoving = false;
        isHover = true;
        if (menu_open != null)
        {
            
            menu_open.gameObject.SetActive(true);
           
        }    
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        if (menu_open != null)
        {
            
            menu_open.gameObject.SetActive(false);
           
            
        }
        IsMoving = true;

    }
    public void Update()
    {
        if (player == null ||isHover)
        {
            return;

        }
       
            UnityEngine.Vector3 mousePos = Input.mousePosition;

            mousePos.z = Camera.main.nearClipPlane;
            UnityEngine.Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);
            //restrict moving only on the x axis: player.transform.position.y

            UnityEngine.Vector3 mouseNext = new UnityEngine.Vector3(worldMouse.x, player.transform.position.y, worldMouse.z);
        if (MenuManager.Instance.current_menu.activeSelf == false)
        {
           
                float distance = mouseNext.x - player.transform.position.x;
                setFacingDirection(distance);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, UnityEngine.Vector2.zero);
                if ((hit.collider != null && hit.collider.gameObject == player))
                {
                   IsMoving = false;
                    return;
                }
                else
                {
                    IsMoving = true;
                    player.transform.position = UnityEngine.Vector3.MoveTowards(player.transform.position, mouseNext, Time.deltaTime * 50f);
                }
            
        }

            
        else
          {
            IsMoving = false;
            return;
                //rb.velocity = UnityEngine.Vector3.zero;
          }
        
        //CheckIfPlayerArrived();
    }

    private void setFacingDirection(float distance)
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
