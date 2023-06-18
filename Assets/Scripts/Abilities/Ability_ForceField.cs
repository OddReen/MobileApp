using UnityEngine;

public class Ability_ForceField : MonoBehaviour
{
    public static Ability_ForceField instance;

    public GameObject abilityButton;
    public float radius;
    public float damage;
    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackTime;

    private void Awake()
    {
        instance ??= this;
    }
    public void OnForceField()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].CompareTag("Enemy"))
            {
                collider2Ds[i].gameObject.GetComponent<HealthSystem_Enemy>().Damage(damage);
                collider2Ds[i].gameObject.GetComponent<HealthSystem_Enemy>().KnockBack(collider2Ds[i].gameObject.transform.position - transform.position, knockBackForce, knockBackTime);
            }
        }
        abilityButton.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
