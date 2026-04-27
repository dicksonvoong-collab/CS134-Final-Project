using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private float explosionLife = 0.5f;
    public float explosionRadius = 5f;
    public int damage = 1;

    // explosion immediately blows up and deals damage
    void Start()
    {
        Destroy(gameObject, explosionLife);
        dealDamage();
    }

    //damage logic
    void dealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyStats enemyHealth = hit.GetComponent<EnemyStats>();

                if (enemyHealth != null)
                {
                    enemyHealth.takeDamage(damage);
                }
                
            }
        }
    }
}
