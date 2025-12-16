using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;
    public bool isInstaKill = false;
    public bool showTakeDamageUI = false;
    public float dieDelay = 0.5f;
    public float takeDamageUIDuration = 2f;
    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            StartCoroutine(DeathManager.deathManager.HandleDeathCoroutine(source));
            Die();
        }
        else
        {
            if (showTakeDamageUI)
            {
                StartCoroutine(TakeDamageUI());
            }
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
            if (showTakeDamageUI)
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
