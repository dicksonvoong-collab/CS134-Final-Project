using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectilePosition;

    public AudioSource audioSource;
    public AudioClip projectileSound;

    public float projectileSpeed;
    public float fireRate;
    private float nextProjectile;

    public int numProjectiles;
    public float attackRange;
    public LayerMask enemys;

    //sets base stats
    private void Start()
    {
        projectileSpeed = 40f;
        fireRate = 0.75f;
        nextProjectile = 1f;

        numProjectiles = 2;
        attackRange = 20f;
    }

    // automatically shoots at nearby enemies, plays shoot audio
    void Update()
    {
        if (Time.time >= nextProjectile)
        {
            List<Transform> targets = GetNearestEnemies(numProjectiles);
            if (targets.Count > 0)
            {
                foreach (Transform target in targets)
                {
                    Shoot(target);
                }

                if (audioSource != null && projectileSound != null)
                {
                    audioSource.pitch = Random.Range(0.9f, 1.1f);
                    audioSource.PlayOneShot(projectileSound);
                }
            }

            nextProjectile = Time.time + fireRate;
        }
    }

    //gets the nearest enemies to shoot at
    List<Transform> GetNearestEnemies(int count)
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, attackRange, enemys);

        List<Collider> enemyList = new List<Collider>(enemiesInRange);
        List<Transform> targets = new List<Transform>();

        while (targets.Count < count && enemyList.Count > 0)
        {
            Collider closest = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider enemy in enemyList)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = enemy;
                }
            }

            if (closest != null)
            {
                targets.Add(closest.transform);
                enemyList.Remove(closest);
            }
        }

        return targets;
    }

    //shoot logic
    void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectilePosition.position, projectilePosition.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (target.position - projectilePosition.position).normalized;
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
        }
    }
}
