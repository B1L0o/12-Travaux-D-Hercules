using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator FadeSystem;
    public AudioSource Opendoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
            SceneManager.LoadScene(sceneName);
            
        }
    }

    public IEnumerator loadNextScene()
    {
        Opendoor.Play();
        FadeSystem.SetTrigger("FadeIn");
        yield return  new WaitForSeconds(1f);
    }
    
    
}
