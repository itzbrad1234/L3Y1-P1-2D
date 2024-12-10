using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timerTxt;
    public float timer;

    [Header("Health")]
    public Slider healthSlider;
    public int maxHealth;
    public int currentHealth;

    [Header("shooting")]
    public Transform shootingPoint;
    public GameObject bullet;
    bool isFacingRight;

    [Header("Main")]
    public float moveSpeed;
    public float jumpForce;
    float inputs;
    public Rigidbody2D rb;
    public float groundDistance;
    public LayerMask layerMask;

    RaycastHit2D hit;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = maxHealth;

        startPos = transform.position;

        currentHealth = maxHealth;
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerTxt.text = timer.ToString("F2");
        
        Movement();
        Health();
        shoot();
        MovementDirection();
        UpdateAnimations();
    }

    void Movement()
    {
        inputs = Input.GetAxisRaw("Horizontal");
        rb.velocity = new UnityEngine.Vector2(inputs * moveSpeed, rb.velocity.y);

        hit = Physics2D.Raycast(transform.position, -transform.up, groundDistance, layerMask);
        Debug.DrawRay(transform.position, -transform.up * groundDistance, Color.yellow);

        if (hit.collider)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

void Health()
{
    healthSlider.value = currentHealth;

    if (currentHealth <= 0)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

void shoot()
{
    if (Input.GetKeyDown(KeyCode.X))
    {
        Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
    }
}

void MovementDirection()
{
    if (isFacingRight && inputs < -.1f)
    {
        flip();
    }
    else if (!isFacingRight && inputs > .1f)
    {
        flip();
    }
}

void flip()
{
    isFacingRight = !isFacingRight;
    transform.Rotate(0f, 180f, 0f);
}

void UpdateAnimations()
{
    Animator anim = transform.GetChild(0).transform.GetComponent<Animator>();

    anim.SetBool("isGrounded", hit.collider);

    if (inputs != 0)
    {
        anim.SetBool("isMoving", true);
    }
    else
    {
        anim.SetBool("isMoving", false);
    }
}

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            transform.position = startPos;
        }
        if (other.gameObject.CompareTag("Exit"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
         if (other.gameObject.CompareTag("Enemy"))
         {
            currentHealth--;
            Destroy(other.gameObject);
         }
    }
}
