using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform enemies;
    [SerializeField] private GameObject bullet;

    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float knockBackForce;
    [SerializeField] protected float knockBackTime;
    [SerializeField] protected float lifeSpan;

    public void OnShootButtonPress()
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
            rb2d.velocity = dir * speed;
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
