using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int damageFromEnemy = 1;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float blinkInterval = 0.1f;

    public static int currentHealth;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityFlash());
    }

    void Die()
    { 
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(damageFromEnemy);
        }
        if (other.gameObject.CompareTag("HarmSpike"))
        {
            TakeDamage(damageFromEnemy);
        }
    }

    private System.Collections.IEnumerator InvincibilityFlash()
    {
        isInvincible = true;

        float timer = 0f;
        while (timer < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }
}
