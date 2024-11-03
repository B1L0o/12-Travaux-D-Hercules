using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastZoneToPlaySound : MonoBehaviour
{
    public AudioManager AudioManager;
    public string SongName;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && AudioManager.IsPlaying(SongName)==false)
        {
            foreach (var sound in AudioManager.Sounds)
            {
                if (AudioManager.IsPlaying(sound.name))
                {
                    AudioManager.Pause(sound.name);
                }
            }
            AudioManager.Play(SongName);
            StartCoroutine(WaitSoundToFinish());
        }
    }

    IEnumerator WaitSoundToFinish()
    {
        while (AudioManager.IsPlaying(SongName))
        {
            yield return null;
        }

        SceneManager.LoadScene("lvl 1");
    }
    
}
