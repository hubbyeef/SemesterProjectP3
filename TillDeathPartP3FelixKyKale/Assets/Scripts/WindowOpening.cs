using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpening : MonoBehaviour
{
    public bool beingOpened;
    public float stopPoint = -0.3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beingOpened)
        {
            if (transform.position.y < stopPoint)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
    }
}
