using System.Collections;
using UnityEngine;
using TMPro;


public class Manager_Wave : MonoBehaviour
{
    public static Manager_Wave instance;

    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float waveBuffMultiplier;
    [SerializeField] private float waveBuffHealthMultiplier;
    [SerializeField] private float waveBuffDamageMultiplier;
    [SerializeField] private float waveBuffSpeedMultiplier;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private int waveNum;
    [SerializeField] private float timeBetweenWaves;
    [Range(0, 100)]
    [SerializeField] private int maxEnemyQuantity;
    [SerializeField] private int currentEnemyQuantity;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform enemies;
    [SerializeField] private Coroutine spawnEnemy;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        StartCoroutine(TimeBetweenWaves(timeBetweenWaves));
    }
    public void VerifyEnemiesNumber()
    {
        currentEnemyQuantity--;
        if (currentEnemyQuantity <= 0)
        {
            StartCoroutine(TimeBetweenWaves(timeBetweenWaves));
        }
    }
    private IEnumerator TimeBetweenWaves(float timer)
    {
        currentEnemyQuantity = maxEnemyQuantity;
        waveText.gameObject.SetActive(true);
        waveNum++;
        waveText.text = "Wave " + waveNum;
        yield return new WaitForSeconds(timer);
        waveText.gameObject.SetActive(false);
        SpawnWave();
    }
    private void SpawnWave()
    {
        for (int i = 0; i < maxEnemyQuantity; i++)
        {
            // Generate a random angle in radians
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);

            // Calculate a random distance within the specified limits
            float randomDistance = Random.Range(minDistance, maxDistance);

            // Calculate the spawn position offset from the player
            Vector2 spawnOffset = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomDistance;

            // Calculate the spawn position relative to the player
            Vector2 spawnPosition = (Vector2)player.position + spawnOffset;

            GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity, enemies);
            AttackSystem_Enemy attackSystem = newEnemy.GetComponent<AttackSystem_Enemy>();
            HealthSystem_Enemy healthSystem = newEnemy.GetComponent<HealthSystem_Enemy>();
            BehaviourSystem_Enemy enemyBehaviour = newEnemy.GetComponent<BehaviourSystem_Enemy>();

            float waveHealthBuff = waveNum * waveBuffHealthMultiplier;
            float waveDamageBuff = waveNum * waveBuffDamageMultiplier;

            healthSystem.currentHealth = waveHealthBuff;
            healthSystem.maxHealth = waveHealthBuff;
            attackSystem.damage = waveDamageBuff;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, minDistance);
        Gizmos.DrawWireSphere(player.position, maxDistance);
    }
}
