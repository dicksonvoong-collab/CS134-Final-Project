using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject deathEffect;
    public HealthBarScript healthUI;
    public AudioSource bgm;

    public GameObject lossScreen;

    // set health and health bar at start of game
    void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        healthUI.updateHealth(currentHealth, maxHealth);
    }

    //player taking damage logic, dies if no health
    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthUI.updateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            die();
        }
    }

    //player healing logic
    public void heal(int health)
    {
        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth -= currentHealth - maxHealth;
        }

        healthUI.updateHealth(currentHealth, maxHealth);
    }

    //blows up when dies, lose screen logic
    public void die()
    {
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        bgm.enabled = false;
        lossScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
}
