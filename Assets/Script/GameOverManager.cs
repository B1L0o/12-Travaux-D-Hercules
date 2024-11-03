
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public string MainMenu;
    public GameObject gameOverUI;
    public static GameOverManager instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager !!!!");
            return;
        }

        instance = this;
    }

    
    public void WhenPlayerDeath()
    {
        //if (CurrentSceneManager.instance.isPlayerPresentByDefault)
            //{
            //DontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        //}
        gameOverUI.SetActive(true);
    }

    
    
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //gameOverUI.SetActive(false);
    }

    
    public void RetryButtonMulti()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //gameOverUI.SetActive(false);
    }

    public void MainMenuButtonMulti()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(MainMenu);
    }

    public void MainMenuButton()
    {
        //DontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene(MainMenu);
    }

    public void QuitMulti()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
