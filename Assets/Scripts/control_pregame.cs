using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Player;
using Unity.VisualScripting;
using UnityEngine;

public class control_pregame : MonoBehaviour
{
    [SerializeField] PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement == null)
        {
            PlayerMovement = GameObject.Find("Movement").GetComponent<PlayerMovement>();
            PlayerMovement.CurrentControl.load_sliders();
        }
        PlayerMovement.CurrentControl.select_sliders();
        PlayerMovement.CurrentControl.UpdateUI();
        
    }
}
