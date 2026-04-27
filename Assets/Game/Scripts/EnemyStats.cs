using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemySpawn spawner;
    public PlayerEXP playerXP;
    public int health;
    public int damage;
    public int expValue;

    //enemy death logic
    public void die()
    {
        spawner.OnEnemyDeath();

        if (playerXP != null)
        {
            playerXP.addExperience(expValue);
        }

            Destroy(gameObject);
    }

    //enemy dealing damage to player logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            player.takeDamage(damage);
        }
    }

    //enemy taking damage logic
    public void takeDamage(int damage)
    {
        health -= damage;
        
        if (health < 0)
        {
            die();
        }
    }
}
