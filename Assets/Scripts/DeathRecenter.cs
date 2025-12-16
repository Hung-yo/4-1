using UnityEngine;

public class DeathRecenter : Death
{
    private Health health;
    public AudioSource deathClip;
    public Vector3 spawnPoint;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        player = GetComponent<Player>();
        //spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Die()
    {
        Debug.Log("Player Died, teleported to origin");
        // Sets pawn rotation and speed back to 0 when respawning
        transform.position = spawnPoint;
        health.currentHealth = health.maxHealth;
    }
}
