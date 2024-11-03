using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public string SettingMenu;
    public string MainMenu;
    public GameObject pauseMenuUI;
    public Player _Player;
    
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        AudioListener.volume = 0f;

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale =  1f;
        gameIsPaused = false;
        AudioListener.volume = 1f;
    }
    
    public void SettingMenuButton()
    {
        //DontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene(SettingMenu);
    }
    public void SettingMenuButtonMulti()
    {
        PhotonNetwork.Disconnect();
        //DontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene(SettingMenu);
    }
    
    public void MainMenuButton()
    {
        //DontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene(MainMenu);
    }
    public void MainMenuButtonMulti()
    {
        PhotonNetwork.Disconnect();
        //DontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene(MainMenu);
    }
}
