using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    Rigidbody2D rb2d;
    Color defaultColor;
    SpriteRenderer spriteRenderer;

    [SerializeField] int scoreAmount;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth;
    [SerializeField] private float redTime = .2f;
    public bool isKnockBacked = false;

    private float CurrentHealth
    {
        get { return currentHealth; }
        set { Mathf.Clamp(value, 0, maxHealth); }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        currentHealth = maxHealth;
    }
    public void DamageEnemy(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            WaveSystem.instance.VerifyEnemiesNumber();
            ScoreSystem.instance.AddScore(scoreAmount);
            Destroy(gameObject);
        }
        VisibleDamage();
    }
    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            MenuSystem.instance.GameOver();
            Destroy(gameObject);
        }
        VisibleDamage();
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
    }
    public void KnockBack(Vector2 direction, float force, float knockBackTime)
    {
        isKnockBacked = true;
        Coroutine coroutine = StartCoroutine(ReverseKnockBack(knockBackTime));
        rb2d.velocity = direction.normalized * force;
    }
    public void VisibleDamage()
    {
        spriteRenderer.color = Color.red;
        Coroutine coroutine = StartCoroutine(ReverseColor(redTime));
    }
    public void Bleed()
    {
        currentHealth -= Time.deltaTime * 10;
    }

    public void Invincibility(float invincibilityTime)
    {
        Coroutine coroutine = StartCoroutine(ReverseInvincibility(invincibilityTime));
    }

    private IEnumerator ReverseInvincibility(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
    }
    private IEnumerator ReverseKnockBack(float knockBackTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        isKnockBacked = false;
    }
    private IEnumerator ReverseColor(float redTime)
    {
        yield return new WaitForSeconds(redTime);
        spriteRenderer.color = defaultColor;
    }
}
