using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem_Enemy : HealthSystem
{
    [SerializeField] private int scoreAmount;
    [SerializeField] private GameObject XPPref;
    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (Random.value < .5f)
            {
                Instantiate(XPPref, transform.position, Quaternion.identity);
            }
            Manager_Wave.instance.VerifyEnemiesNumber();
            Manager_Score.instance.AddScore(scoreAmount);
            Destroy(gameObject);
        }
        VisibleDamage();
    }
}
