using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TheMonster : MonoBehaviour
{
    private GameObject player;
    public Animator animator;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator.speed = 1;
        animator.Play("locom_f_basicWalk_30f");
        speed = 5f;
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
