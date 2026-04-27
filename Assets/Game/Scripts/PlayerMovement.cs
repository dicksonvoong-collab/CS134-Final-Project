using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;

    public float speed;
    public float jumpForce;
    public float turnSpeed;

    public bool isGrounded;
    public float groundDistance;
    public LayerMask groundLayer;

    private float timer;
   
    private Vector3 moveDirection;

    //set base movement stats
    void Start()
    {
        speed = 10f;
        jumpForce = 8f;
        turnSpeed = 8f;

        groundDistance = 0.8f;
    }

    // update checks for jumping
    private void Update()
    {
        isGrounded = Physics.SphereCast(transform.position, 0.4f, Vector3.down, out _, groundDistance, groundLayer);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump();
        }
    }

    //fixed update checks for player movement, resets speed after speed buffs
    void FixedUpdate()
    {
        Movement();

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
        }

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
        }

        if (Time.time >= timer)
        {
            speed = 10f;
        }
    }

    //gets move direction based on camera direction, 
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        moveDirection = (horizontal * camRight + vertical * camForward).normalized;
        moveDirection.y = 0f;

        if (moveDirection != Vector3.zero)
        {
            //move the player
            Vector3 velocity = moveDirection * speed;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    //jump logic
    void jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    //speed buff logic
    public void speedUp(int speedTo, int time)
    {
        speed = speedTo;
        timer = Time.time + time;
    }
}
