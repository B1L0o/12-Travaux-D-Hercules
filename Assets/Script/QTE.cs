using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour
{
    public Text displayText;
    public Text passText;
    public float timeToRespond;
    public float timeBeforeVictory;
    private float actualTime;
    public List<KeyCode> listPossibleKeys;
    private bool waitingForAKey;
    private KeyCode correctKey;
    private int indexCorrectKey;
    private bool keyFound;
  
    
    
    void Update()
    {
        if (timeBeforeVictory>0)
        {
            timeBeforeVictory -= Time.deltaTime;
        }
        else
        {
            VictoryManager.instance.WhenBossDeath();
            enabled = false;
        }
        
        if (waitingForAKey==false)
        {
            indexCorrectKey = Random.Range(0, listPossibleKeys.Count);
            correctKey = listPossibleKeys[indexCorrectKey];
            waitingForAKey = true;
            actualTime = timeToRespond;
            passText.text = "";
            displayText.text = correctKey.ToString();
            keyFound = false;


        }
        else
        {
            if (actualTime>0)
            {
                if (keyFound==false)
                {
                    actualTime -= Time.deltaTime;
                    if (Input.anyKeyDown)
                    {
                        if (Input.GetKeyDown(correctKey))
                        {
                            passText.text = "SUCCESS !!!";
                            keyFound = true;
                            StartCoroutine(WaitBetweenTwoKey());
                        }
                        else
                        {
                            passText.text = "FAIL !!!";
                            GameOverManager.instance.WhenPlayerDeath();
                            enabled = false;
                        }
                }
                    
                }
            }
            else
            {
                passText.text = "FAIL !!!";
                GameOverManager.instance.WhenPlayerDeath();
                enabled = false;
            }
            
        }
    }

    IEnumerator WaitBetweenTwoKey()
    {
        yield return new WaitForSeconds(1.5f);
        waitingForAKey = false;
    }

    
}
