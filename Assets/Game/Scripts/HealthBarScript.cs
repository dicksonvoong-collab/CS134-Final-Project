using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Slider healthBar;
    public PlayerHealth playerHealth;

    // set health bar color
    void Start()
    {
        healthBar.fillRect.GetComponent<Image>().color = Color.green;
    }

    //updates health when health change
    public void updateHealth(int currentHealth, int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        healthText.text = currentHealth + "/" + maxHealth;
    }
}
