using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentOpen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public bool beingOpened;
    public float openSpeed;
    public float maxOpenDistance = 5;
    public Transform hingePoint;

    public bool open;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenVent()
    {
        transform.Rotate(new Vector3(90, 0, 0));
    }

    public void CloseVent()
    {
        transform.Rotate(new Vector3(-90, 0, 0));
    }

    void OnMouseOver()
    {
        {
                float dist = Vector3.Distance(player.transform.position, transform.position);
                if (dist < 5)
                {

                    {
                        if (open == true )
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                CloseVent();
                            }
                        }

                    }

                }
            

        }

    }
}
