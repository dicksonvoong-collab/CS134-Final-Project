using UnityEngine;

public class EnemyJumpMovement : MonoBehaviour
{
    public Transform playerLocation;
    public Rigidbody rb;

    public float groundDistance = 1.2f;
    public LayerMask groundLayer;

    private float nextHop;
    private float hopTime = 3f;

    public float hopHeight = 8f;
    public float hopForce = 6f;

    // sets starting hop time
    void Start()
    {
        nextHop = Time.time + Random.Range(0, hopTime);
    }

    // hops toward player when able
    void Update()
    {
        if (playerLocation == null)
        {
            return;
        }

        Vector3 targetDir = playerLocation.position - transform.position;
        targetDir.y = 0;

        if (targetDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(targetDir);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);
        }

        bool isGrounded = Physics.SphereCast(transform.position + Vector3.up, 0.5f, Vector3.down, out _, groundDistance, groundLayer);

        if (Time.time >= nextHop && isGrounded)
        {
            hop();
            nextHop = Time.time + Random.Range(0, hopTime);
        }

        
    }

    //hop logic
    void hop()
    {
        Vector3 direction = (playerLocation.position - transform.position).normalized;
        direction.y = 0;

        transform.forward = direction;

        rb.linearVelocity = Vector3.zero;
        
        rb.AddForce(Vector3.up * hopHeight, ForceMode.Impulse);
        rb.AddForce(direction * hopForce, ForceMode.Impulse);
    }
}
