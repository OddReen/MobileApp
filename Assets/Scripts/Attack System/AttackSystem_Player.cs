using System.Collections;
using UnityEngine;

public class AttackSystem_Player : AttackSystem
{
    [SerializeField] private Transform enemies;
    [SerializeField] private GameObject bullet;

    public float timeBetweenShots;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletLifeSpan;

    public float TimeBetweenShots
    {
        get { return timeBetweenShots; }
        set { Mathf.Min(0.1f); }
    }

    private void Awake()
    {
        Coroutine coroutine = StartCoroutine(TriggerShoot(timeBetweenShots));
    }
    private IEnumerator TriggerShoot(float timeBetweenShots)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            OnShoot();
        }
    }
    public void OnShoot()
    {
        Transform closestEnemyPos = GetClosestEnemyOnShoot(enemies);
        if (closestEnemyPos != null)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            Debug.Log(closestEnemyPos);
            Vector3 direction = closestEnemyPos.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Rigidbody2D rb2d = newBullet.GetComponent<Rigidbody2D>();
            Vector2 dir = (closestEnemyPos.position - transform.position).normalized;
            rb2d.velocity = dir * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("No enemies found.");
        }
    }
    Transform GetClosestEnemyOnShoot(Transform enemiesTransform)
    {
        Transform tMin = null;
        if (enemiesTransform.childCount == 0)
        {
            return tMin;
        }
        float minDist = Vector3.Distance(enemiesTransform.GetChild(0).position, transform.position); // Initialize with the distance to the first enemy
        foreach (Transform enemy in enemiesTransform)
        {
            float dist = Vector3.Distance(enemy.position, transform.position);
            if (dist <= minDist)
            {
                tMin = enemy;
                minDist = dist;
            }
        }
        return tMin;
    }
}
