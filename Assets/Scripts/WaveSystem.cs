using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveSystem : MonoBehaviour
{
    public static WaveSystem instance;

    [SerializeField] private float waveBuffMultiplier;
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
            float randFloatX = Random.Range(-15f, 15f);
            float randFloatY = Random.Range(-15f, 15f);
            Vector2 randVector = new Vector2(randFloatX + player.position.x, randFloatY + player.position.y);
            GameObject newEnemy = Instantiate(enemy, randVector, Quaternion.identity, enemies);
            HealthSystem healthSystem = newEnemy.GetComponent<HealthSystem>();
            EnemyBehaviour enemyBehaviour = newEnemy.GetComponent<EnemyBehaviour>();
            float waveBuff = waveNum * waveBuffMultiplier;
            healthSystem.currentHealth += waveBuff;
            healthSystem.maxHealth += waveBuff;
            enemyBehaviour.damageAmount += waveBuff;
            enemyBehaviour.speed += waveBuff;
        }
    }
}
