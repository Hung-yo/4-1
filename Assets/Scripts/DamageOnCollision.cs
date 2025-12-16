using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 3;
    public bool hasCollided = false;
    public bool destroyOnCollision = false;
    public string damageSource = "Cactus";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && destroyOnCollision)
        {
            OnDestroy();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageAmount, damageSource);
            hasCollided = true;
        }
    }
    
    void OnDestroy()
    {
        Death death = GetComponent<Death>();
        if (death != null)
        {
            death.Die();
        }
    }
}
