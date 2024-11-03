using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string sceneToGo;
    public void OnEnable()
    {
        SceneManager.LoadScene(sceneToGo);
    }
}
