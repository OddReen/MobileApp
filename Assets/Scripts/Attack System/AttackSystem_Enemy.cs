using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem_Enemy : AttackSystem
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem_Player healthSystem = collision.gameObject.GetComponent<HealthSystem_Player>();
            healthSystem.Damage(damage);
            healthSystem.KnockBack(collision.transform.position - transform.position, knockBackForce, knockBackStunTime);
        }
    }
}
