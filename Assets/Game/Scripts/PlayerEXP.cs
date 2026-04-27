using UnityEngine;

public class PlayerEXP : MonoBehaviour
{
    public int currentLevel;
    public int currentEXP;
    public int expToNextLevel;

    public PlayerHealth playerHealth;
    public ExplosionScript explosionScript;

    public EXPBarScript expBar;
    public HealthBarScript healthBar;

    //start of game stats
    private void Start()
    {
        explosionScript.damage = 1;
        currentLevel = 1;
        currentEXP = 0;
        expToNextLevel = 75;
    }

    //exp gain logic
    public void addExperience(int amount)
    {
        currentEXP += amount;

        while (currentEXP >= expToNextLevel)
        {
            levelUp();
        }

        expBar.updateUI();
    }

    //level up logic
    void levelUp()
    {
        currentEXP -= expToNextLevel;
        currentLevel++;

        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.3f);

        playerHealth.currentHealth = Mathf.CeilToInt(playerHealth.currentHealth * 1.1f);
        playerHealth.maxHealth = Mathf.CeilToInt(playerHealth.maxHealth * 1.1f);
        explosionScript.damage = Mathf.CeilToInt(explosionScript.damage * 1.1f);

        healthBar.updateHealth(playerHealth.currentHealth, playerHealth.maxHealth);
    }

}
