using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SafeSpot : MonoBehaviour
{
    public ClosetopencloseDoor closetDoor;
    public ClosetopencloseDoor closetBase;
    public bool closetOpen;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (closetDoor.open == false && closetBase.open == false)
        {
            closetOpen = false;
        }

        else if (closetDoor.open == true || closetBase.open == true)
        {
            closetOpen = true;
        }
    }


}
