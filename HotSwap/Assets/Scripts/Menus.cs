using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    GameObject gm = null;
    GameManager gmScript = null;

    public bool paused;

    public GameObject pauseMenu;
    public GameObject quitMenu;

    void Awake()
    {
        gm = GameObject.Find("GameManager");
        gmScript = gm.GetComponent<GameManager>();

        pauseMenu = GameObject.Find("PauseMenu");
        quitMenu = GameObject.Find("QuitMenu");
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            paused = paused ? false : true;
            gmScript.paused = paused;
            PauseMenu();
        }


    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        LoadScene("MainMenu");
    }

    public void Quit()
    {
        quitMenu.SetActive(true);
    }
    public void confirmedQuit()
    {
        Application.Quit();
    }
    public void canceledQuit()
    {
        quitMenu.SetActive(false);
    }

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void CharacterSelect()
    {
        //?
    }

    public void PauseMenu()
    {
        if( Time.timeScale == 1 )
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            paused = true;
            
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            paused = false;
        }

    }

}
