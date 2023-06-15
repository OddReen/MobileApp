using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Shooting
{
    private void Awake()
    {
        Coroutine coroutine = StartCoroutine(ReverseColor(lifeSpan));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthSystem>().DamageEnemy(damage);
            collision.gameObject.GetComponent<HealthSystem>().KnockBack(collision.gameObject.transform.position - transform.position, knockBackForce, knockBackTime);
            Destroy(gameObject);
        }
    }

    private IEnumerator ReverseColor(float lifeSpan)
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
