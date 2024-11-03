using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public TMP_Dropdown Dresolution;
    public Toggle Fullscreen;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("IntroCinematic");
    }

    public void Options()
    {
        SceneManager.LoadScene("Settings Menu");

    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    


    public void SelectLevel()
        {
            SceneManager.LoadScene("Select Level Menu");
    
        }
    public void level1()
    {
        SceneManager.LoadScene("lvl 1");
    }
    public void level1Multi()
    {
        SceneManager.LoadScene("lvl 1 multi final");
    }
        public void level2()
        {
            SceneManager.LoadScene("Level 2");
    
        }
        public void level2Multi()
        {
            SceneManager.LoadScene("Level 2 multiplayer");
    
        }
        
        public void level3()
        {
            SceneManager.LoadScene("lvl 3");
    
        }
        public void level3Multi()
        {
            SceneManager.LoadScene("lvl 3 multiplayer");
    
        }
        public void level4()
        {
            SceneManager.LoadScene("Level 4");
        }
        public void level4Multi()
        {
            SceneManager.LoadScene("Level 4 multiplayer");
        }
        
        public void level5()
        {
            SceneManager.LoadScene("lvl 5");
    
        }
        
        public void level6()
        {
            SceneManager.LoadScene("lvl 6");
        }
        public void tutorial()
        {
            SceneManager.LoadScene("tutorial");
    
        }
        
        public void back2()
        {
            SceneManager.LoadScene("Main Menu");
    
        }
        public void back3()
        {
            SceneManager.LoadScene("Multiplayer Menu");
    
        }
        public void level7()
        {
            SceneManager.LoadScene("lvl 7-1");
        }
        public void level7Multi()
        {
            SceneManager.LoadScene("lvl 7-1 multiplayer");
        }
        public void level8()
        {
            SceneManager.LoadScene("lvl 8-1");
        }
        public void level8Multi()
        {
            SceneManager.LoadScene("lvl 8-1 multiplayer");
        }
        public void level9()
        {
            SceneManager.LoadScene("lvl 9-1");
        }
        public void level10()
        {
            SceneManager.LoadScene("lvl 10-1");
        }
        public void level10Multi()
        {
            SceneManager.LoadScene("lvl 10-1 multiplayer");
        }
        public void level11()
        {
            SceneManager.LoadScene("lvl 11-2");
        }
        public void level12()
        {
            SceneManager.LoadScene("lvl 12-1");
        }
        public void level12Multi()
        {
            SceneManager.LoadScene("lvl 12-1 multiplayer");
        }

        public void multiplayer()
        {
            SceneManager.LoadScene("Multiplayer Menu");
        }

        public void host()
        {
            PlayerPrefs.SetInt("host",1);
            SceneManager.LoadScene("Select Level Menu multi");
            //SceneManager.LoadScene("Level 2 multiplayer");
        }
        
        public void join()
        {
            PlayerPrefs.SetInt("host",0);
            SceneManager.LoadScene("Select Level Menu multi");
            //SceneManager.LoadScene("Level 2 multiplayer");
        }
        
        
}
