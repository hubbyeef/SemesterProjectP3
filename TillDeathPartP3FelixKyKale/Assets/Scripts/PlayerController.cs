using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody rb;
    private Camera mainCam;

    public float horizontal;
    public float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        PlayerMovement();
    }

    void PlayerMovement()
    {
        rb.velocity = (Vector3.left * speed * horizontal);
        rb.velocity = (Vector3.forward  * speed * vertical);
    }
}
