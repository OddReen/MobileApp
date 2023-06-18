using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem_Player : HealthSystem
{
    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Manager_Menus.instance.GameOver();
            Destroy(gameObject);
        }
        VisibleDamage();
    }
}
