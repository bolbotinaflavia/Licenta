using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GetPlayerForCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        this.gameObject.GetComponent<CinemachineVirtualCamera>().Follow = player.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
