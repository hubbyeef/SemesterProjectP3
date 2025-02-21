using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Range(0, 3000)]
    public float stamina;
    public float maxStamina = 2000;
    private float staminaSpendRate = 120f;
    private float staminaRechargeRate = 80;
    public float speed = 25f;
    public bool running;
    private Coroutine recharge;

    public Image staminaBar;


    public Animator animator;
    public GameManager gameManager;
    private Rigidbody rb;
    private Camera mainCam;

    public float horizontal;
    public float vertical;

    Vector3 moveDirection;
    public Transform orientation;

    public float staminaPercentUnit;
    public float percentUnit;

    public bool safe;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCam = Camera.main;

        stamina = maxStamina;
    }

    private void Update()
    {

    }

    private void OnValidate()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rb.drag = 5;
        PlayerMovement();
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            StopCoroutine(RechargeStamina());
            speed = 32; //sprint
            running = true;

            stamina -= staminaSpendRate * Time.deltaTime;
            if (stamina <0) { stamina = 0; speed = 25; }
            UpdateStamina();
        }
        else if (stamina < maxStamina)
        {
            speed = 25;
            running = false;
            StartCoroutine(RechargeStamina());
            if (stamina > maxStamina) 
            { stamina = maxStamina; StopCoroutine(RechargeStamina()); }
        }

        
        
        
    }

    void PlayerMovement()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        transform.rotation = orientation.rotation;
        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);

        animator.SetFloat("Move X", Mathf.Abs(vertical));
    }

    void UpdateStamina()
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }

    IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1);

        if (stamina < maxStamina)
        {
            stamina += staminaRechargeRate / 150;
            UpdateStamina();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            Destroy(other);
            StartCoroutine(gameManager.GameOver());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<SafeSpot>())
        {
            safe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SafeSpot>())
        {
            safe = false;
        }
    } 
}
