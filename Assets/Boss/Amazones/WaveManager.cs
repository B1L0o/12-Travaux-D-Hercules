using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    //[SerializeField] 
    //private SpawnerAmazones spawnerAmazones;
    [SerializeField] 
    private int[] numberOfAmazonesForEachWave;
    private List<GameObject> listCurrentAmazones;
    private int numberActualWave;
    [SerializeField] 
    private Text textWave;

    [SerializeField]
    private Player player;
    [SerializeField] 
    private Transform spawnPoint2;

    // Start is called before the first frame update

    private void StartWave(int numberOfAmazones)
    {
        //listCurrentAmazones = spawnerAmazones.SpawnAmazones(numberOfAmazones);
        for (int i = 0; i < numberOfAmazones/2; i++)
        {
            var amazone = ObjectPooler.Instance.SpawnFromPool("amazone", transform.position, transform.rotation);
            Amazone amazoneObject = amazone.GetComponent<Amazone>();
            amazoneObject.Damage = Random.Range(1,3);
            amazoneObject._speed = Random.Range(3f,6f);
            amazoneObject._player = player;
            amazoneObject.SetHealth(200);
            listCurrentAmazones.Add(amazone);

        }

        for (int i = numberOfAmazones/2; i < numberOfAmazones; i++)
        {
            var amazone = ObjectPooler.Instance.SpawnFromPool("amazone", spawnPoint2.position, spawnPoint2.rotation);
            Amazone amazoneObject = amazone.GetComponent<Amazone>();
            amazoneObject.Damage = Random.Range(1,3);
            amazoneObject._speed = Random.Range(3f,6f);
            amazoneObject._player = player;
            amazoneObject.SetHealth(200);
            listCurrentAmazones.Add(amazone);
        }
    }
    void Start()
    {
        listCurrentAmazones = new List<GameObject>();
        numberActualWave = -1;
    }
    
    void Update()
    {
        if (AreAmazonesAllDead() && numberActualWave+1<numberOfAmazonesForEachWave.Length)
        {
            
            numberActualWave = numberActualWave + 1;
            textWave.text = "Wave " + (numberActualWave+1);
            StartWave(numberOfAmazonesForEachWave[numberActualWave]);
        }
        else if (AreAmazonesAllDead() && numberActualWave+1>=numberOfAmazonesForEachWave.Length)
        {
            TriggerVictory();
        }
        
        
    }
    public void TriggerVictory()
    {
        VictoryManager.instance.WhenBossDeath();
    }

    private bool AreAmazonesAllDead()
    {
        listCurrentAmazones.RemoveAll(amazone => amazone.gameObject is null||amazone.CompareTag("Untagged"));
        return  listCurrentAmazones.Count==0;

    }
}
