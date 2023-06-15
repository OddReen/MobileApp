using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody2D rb2d;
    GameObject player;
    HealthSystem healthSystem;

    [Header("Movement")]
    [SerializeField] public float speed = 5f;
    [SerializeField] private float rotationTime = 5f;

    [Header("Attack")]
    [SerializeField] public float damageAmount = 5f;
    [SerializeField] private float knockBackForce = 5f;
    [SerializeField] private float knockBackStunTime = 1f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
    }
    private void FixedUpdate()
    {
        if (!healthSystem.isKnockBacked && player != null)
        {
            Rotation();
            FollowPlayer();
        }
    }
    private void Rotation()
    {
        Vector3 dir = player.transform.position - transform.position;
        float targetRotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float smoothRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, rotationTime * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, smoothRotation);
    }
    private void FollowPlayer()
    {
        Vector2 playerDir = (player.transform.position - transform.position);
        rb2d.velocity = playerDir.normalized * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            healthSystem.DamagePlayer(damageAmount);
            healthSystem.KnockBack(player.transform.position - transform.position, knockBackForce, knockBackStunTime);
        }
    }
}
