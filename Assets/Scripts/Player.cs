using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float baseSpeed;
    public float moveSpeed;

    [Header("Health")]
    public Health health;
    public Death death;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        death = GetComponent<Death>();
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
        // TODO add jumping
    }
}