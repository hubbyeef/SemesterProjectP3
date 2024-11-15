using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 25f;
    public Animator animator;
    private Rigidbody rb;
    private Camera mainCam;

    public float horizontal;
    public float vertical;

    Vector3 moveDirection;
    public Transform orientation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rb.drag = 5;
        PlayerMovement();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 32; //sprint
        }
        else
        {
            speed = 25;
        }
        
    }

    void PlayerMovement()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        transform.rotation = orientation.rotation;
        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);

        animator.SetFloat("Move X", Mathf.Abs(vertical));
    }
}
