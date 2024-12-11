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
    private GameManager gameManager;

    public AudioClip ventOpening;
    public AudioClip ventClosing;

    public AnimationClip openVent;

    public bool open;
    public float timer;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        timer = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator OpenVent()
    {
        audioSource.PlayOneShot(ventOpening);
        yield return new WaitForSeconds(ventOpening.length);
        animator.Play("VentOpening");
        open = true;
    }

    public void CloseVent()
    {
        animator.Play("VentClosing");
        timer = 12.5f;
        open = false;
    }
}
