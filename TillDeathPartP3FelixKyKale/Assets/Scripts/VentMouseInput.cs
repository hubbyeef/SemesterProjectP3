using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentMouseInput : MonoBehaviour
{
    private GameObject player;
    private VentOpen parentHinge;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        parentHinge = GetComponentInParent<VentOpen>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 10)
        {

            {
                if (parentHinge.opened == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        parentHinge.CloseVent();
                    }
                }

            }

        }
    }
}
