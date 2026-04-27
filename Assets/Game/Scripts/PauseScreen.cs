using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseScreen;

    public bool paused;

    //set pause screen inactive at start
    void Start()
    {
        pauseScreen.SetActive(false);
        paused = false;
    }

    //pause and unpause with esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            unpauseGame();
        }
    }

    //pause game logic
    public void pauseGame()
    {
        pauseScreen.SetActive(true);
        AudioListener.pause = true;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    //unpause game logic
    public void unpauseGame()
    {
        pauseScreen.SetActive(false);
        AudioListener.pause = false;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
