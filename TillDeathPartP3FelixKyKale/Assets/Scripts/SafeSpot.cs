using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpot : MonoBehaviour
{
    public ClosetopencloseDoor closetDoor;
    public bool closetOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (closetDoor.open == true)
        {
            closetOpen = true;
        }
        else if (closetDoor.open == false)
        {
            closetOpen = false;
        }
    }


}
