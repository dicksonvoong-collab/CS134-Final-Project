using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScreen : MonoBehaviour
{
    public GameObject lossScreen;
    public AudioSource bgm;

    //no loss screen at start
    void Start()
    {
        lossScreen.SetActive(false);
    }

    //game restart button logic
    public void gameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        bgm.enabled = true;
        Time.timeScale = 1f;
    }
}
