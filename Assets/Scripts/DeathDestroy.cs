using UnityEngine;

public class DeathDestroy : Death
{
    public AudioSource deathClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        if (deathClip != null)
        {
            AudioSource.PlayClipAtPoint(deathClip.clip, transform.position);
        }
        Destroy(gameObject);
    }
}
