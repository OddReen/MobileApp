using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Bullet : AttackSystem_Player
{
    private void Awake()
    {
        Coroutine coroutine = StartCoroutine(ReverseColor(bulletLifeSpan));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthSystem_Enemy>().Damage(damage);
            collision.gameObject.GetComponent<HealthSystem_Enemy>().KnockBack(collision.gameObject.transform.position - transform.position, knockBackForce, knockBackStunTime);
            Destroy(gameObject);
        }
    }

    private IEnumerator ReverseColor(float lifeSpan)
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
