using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    Rigidbody2D rb2d;
    Color defaultColor;
    SpriteRenderer spriteRenderer;

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

    #region Courotines
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
    #endregion
}
