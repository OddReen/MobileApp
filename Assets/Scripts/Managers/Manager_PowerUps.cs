using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_PowerUps : MonoBehaviour
{

    public static Manager_PowerUps instance;

    GameObject player;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject powerUpUI;
    [SerializeField] GameObject[] powerUpGameObjects;
    [SerializeField] Transform[] pos;
    List<int> powerUpNum = new List<int>();

    private void Awake()
    {
        if (gameObject.GetComponent<Button>() == null)
        {
            instance ??= this;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnLevelUp()
    {
        powerUpUI.SetActive(true);
        UI.SetActive(false);
        Time.timeScale = 0f;
        powerUpNum.Clear();
        for (int i = 0; i < pos.Length; i++)
        {
            // Pick up a random PowerUp
            int rand;
            do
            {
                rand = Random.Range(0, powerUpGameObjects.Length);
            }
            while (powerUpNum.Contains(rand));

            powerUpNum.Add(rand);

            //Activate PowerUp
            powerUpGameObjects[rand].SetActive(true);
            powerUpGameObjects[rand].transform.position = pos[i].position;
        }
    }
    public void OnMovingSpeedPowerUp()
    {
        //Moving Speed
        player.GetComponent<BehaviourSystem_Player>().speed++;
        EndPowerUpMenu();
    }
    public void OnBasicDamagePowerUp()
    {
        //Basic Damage
        player.GetComponent<AttackSystem_Player>().damage++;
        EndPowerUpMenu();
    }
    public void OnDamageSpeedPowerUp()
    {
        //Damage Speed
        player.GetComponent<AttackSystem_Player>().timeBetweenShots -= .1f;
        EndPowerUpMenu();
    }
    public void OnForceFieldDamagePowerUp()
    {
        //Force Field Damage
        player.GetComponent<Ability_ForceField>().damage++;
        EndPowerUpMenu();
    }
    public void OnForceFieldAOEPowerUp()
    {
        //Force Field AOE
        player.GetComponent<Ability_ForceField>().radius++;
        EndPowerUpMenu();
    }

    private void EndPowerUpMenu()
    {
        UI.SetActive(true);
        for (int i = 0; i < powerUpGameObjects.Length; i++)
        {
            powerUpGameObjects[i].SetActive(false);
        }
        powerUpNum.Clear();
        Time.timeScale = 1f;
        powerUpUI.SetActive(false);
    }
}
