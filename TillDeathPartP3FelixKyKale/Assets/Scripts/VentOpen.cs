using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentOpen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public bool beingOpened;
    public float openSpeed;
    public float maxOpenDistance = 2;
    public Transform hingePoint;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if (distance < maxOpenDistance)
        {
            
        }
    }

    void CloseVent()
    {
        transform.RotateAround(hingePoint.transform.position, 90);
    }
}
