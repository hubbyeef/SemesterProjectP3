using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentOpen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Transform hingePoint;

    private Animator animator;
    private AudioSource audioSource;

    public AudioClip ventRattling;
    public AudioClip ventOpening;
    public AudioClip ventClosing;

    public AnimationClip openVent;

    public bool opened;
    public float timer;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        timer = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator OpenVent()
    {
        audioSource.PlayOneShot(ventRattling);
        yield return new WaitForSeconds(ventRattling.length);
        animator.Play("VentOpening");
        audioSource.PlayOneShot(ventOpening);
        opened = true;
    }

    public void CloseVent()
    {
        animator.Play("VentClosing");
        timer = 12.5f;
        opened = false;
    }
}
