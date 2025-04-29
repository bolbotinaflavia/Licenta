using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Player;
using UnityEngine;

public class control_pregame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement.Instance.CurrentControl.Move_pregame();
      
            PlayerMovement.Instance.CurrentControl.UpdateUI();
        
    }
}
