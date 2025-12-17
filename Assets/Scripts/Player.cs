using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rigidBody;
    public CapsuleCollider capsuleCollider;
    [Header("Movement")]
    public float baseSpeed;
    public float moveSpeed;
    public float jumpHeight;
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck; // Marker at the player's feet
    public float groundCheckDistance = 0.4f;

    [Header("Health")]
    public Health health;
    public Death death;

    [Header("Fall Damage")]
    private bool wasGrounded;
    private float fallStartHeight;
    private float fallEndHeight;
    public float fallDamageThreshold = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        death = GetComponent<Death>();
        wasGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Sprint key
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = baseSpeed * 2;
        }
        else
        {
            moveSpeed = baseSpeed;
        }

        bool currentlyGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, groundLayer);

        // Note height when leaving the ground
        if (wasGrounded && !currentlyGrounded)
        {
            fallStartHeight = transform.position.y;
        }
        // Note height when touching ground again
        if (!wasGrounded && currentlyGrounded)
        {
            fallEndHeight = transform.position.y;
            // Determine if fall damage is lethal
            float fallDistance = fallStartHeight - fallEndHeight;
            if (fallDistance > fallDamageThreshold)
            {
                health.TakeDamage(10, "Fall_Damage");
            }
        }

        isGrounded = currentlyGrounded;
        wasGrounded = currentlyGrounded;
    }

    public void MoveForward(float moveSpeed)
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void MoveRight(float moveSpeed)
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    public void ToggleKinematic(bool toggle)
    {
        if (!toggle)
        {
            rigidBody.isKinematic = false;
            capsuleCollider.enabled = true;
        }
        else
        {
            rigidBody.isKinematic = true;
            capsuleCollider.enabled = false;
        }
    }
}