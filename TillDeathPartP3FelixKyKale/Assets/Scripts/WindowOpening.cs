using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpening : MonoBehaviour
{
    public bool beingOpened;
    public float stopPoint = -0.3f;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beingOpened)
        {
            animator.Play("Openingwindow");
        }
    }
}
