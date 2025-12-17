using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;
    public bool isInstaKill = false;
    public bool isPlayer = false;
    public float dieDelay = 0.5f;
    public float takeDamageUIDuration = 2f;
    public GameManager gameManager;
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = gameManager.player;
        gameManager = GameManager.gameManager;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInstaKill && currentHealth > 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void TakeDamage(float amount, string source)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (isPlayer)
            {
                player.ToggleKinematic(true);
                StartCoroutine(DeathManager.deathManager.HandleDeathCoroutine(source));
            }
            Die();
        }
        else
        {
            if (isPlayer)
            {
                StartCoroutine(TakeDamageUI());
            }
        }
    }

    public void TakeDamage(float tickAmount, int duration, string source)
    {
        StartCoroutine(TakeDamageOverTime(tickAmount, duration, source));
    }

    private IEnumerator TakeDamageOverTime(float tickAmount, int duration, string source)
    {
        while (duration > 0)
        {
            yield return new WaitForSeconds(duration);
            if (currentHealth == 0)
                break;
            TakeDamage(tickAmount, source);
            duration--;
        }
    }

    private IEnumerator TakeDamageUI()
    {
        gameManager.takeDamageUI.SetActive(true);
        yield return new WaitForSeconds(takeDamageUIDuration);
        gameManager.takeDamageUI.SetActive(false);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator DieWithDelay(float dieDelay)
    {
        Death death = GetComponent<Death>();
        yield return new WaitForSeconds(dieDelay);
        death.Die();
    }

    public void Die()
    {
        Death death = GetComponent<Death>();
        if (death != null)
        {
            if (isPlayer)
            {
                StartCoroutine(DieWithDelay(dieDelay));
            }
            else
            {
                death.Die();
            }
        }
    }
}
