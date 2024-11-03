using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class SpawerBird : MonoBehaviour
{
    public AudioManager audioManager;
    private int numberOfBirdsKilled;
    private int numberOfBirdsLeft;
    public Text textNumberOfBirdsKilled;
    public Text textNumberOfBirdsLeft;
    public GameObject bird;
    public Camera playerCamera;
    public int number;
    // Start is called before the first frame update
    public void SpawnRandom()
    {
        Vector3 screenPosition = playerCamera.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width-40), Random.Range(0,Screen.height-40), playerCamera.farClipPlane/2));
        GameObject Bird = Instantiate(bird,screenPosition,Quaternion.identity);
        BirdBoss birdObject = Bird.GetComponent<BirdBoss>();
        birdObject.playerCamera = playerCamera;
        birdObject.SpawerBird = this;
        Bird.SetActive(true);
    }
    void Start()
    {
        numberOfBirdsLeft = 0;
        numberOfBirdsKilled = 0;
        StartCoroutine(SpawnBirds());
    }
    IEnumerator SpawnBirds()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < number; i++)
        {
            SpawnRandom();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (numberOfBirdsKilled+numberOfBirdsLeft == number)
        {
            Cursor.visible = true;
            if (numberOfBirdsLeft>=numberOfBirdsKilled)
            {
                Cursor.visible = true;
                GameOverManager.instance.WhenPlayerDeath();
            }
            else
            {
                Cursor.visible = true;
                VictoryManager.instance.WhenBossDeath();
            }
            
        }
    }
    
    
    public void IncreaseNumberOfBirdsKilled()
    {
        audioManager.Play("shoot");
        numberOfBirdsKilled += 1;
        textNumberOfBirdsKilled.text = "Number of birds killed : " + numberOfBirdsKilled;
    }
    public void IncreaseNumberOfBirdsLeft()
    {
        numberOfBirdsLeft += 1;
        textNumberOfBirdsLeft.text = "Number of birds that left : " + numberOfBirdsLeft;
    }
    
    
}
