using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_XP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] Image fillerXP;
    [SerializeField] int level;
    [SerializeField] float currentXP;
    [SerializeField] float maxXP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("XP"))
        {
            currentXP++;
            if (currentXP >= maxXP)
            {
                currentXP = 0;
                level++;
                Manager_PowerUps.instance.OnLevelUp();
                Ability_ForceField.instance.abilityButton.SetActive(true);
                textLevel.text = level.ToString();
            }
            float percentageXP = currentXP / maxXP;
            fillerXP.fillAmount = percentageXP;
            Destroy(collision.gameObject);
        }
    }
}
