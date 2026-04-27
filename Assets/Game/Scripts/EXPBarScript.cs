using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarScript : MonoBehaviour
{
    public TextMeshProUGUI expText;
    public Slider expBar;
    public PlayerEXP playerEXP;

    //start of game exp bar set up
    public void Start()
    {
        updateUI();
    }

    //exp bar update logic
    public void updateUI()
    {
        if (expText != null)
        {
            expText.text = "LVL " + playerEXP.currentLevel;
        }

        if (expBar != null)
        {
            expBar.maxValue = playerEXP.expToNextLevel;
            expBar.value = playerEXP.currentEXP;
        }
    }
}
