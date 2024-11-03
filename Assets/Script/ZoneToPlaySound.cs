using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneToPlaySound : MonoBehaviour
{
    public AudioManager AudioManager;
    public string SongName;
   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            foreach (var sound in AudioManager.Sounds)
            {
                if (AudioManager.IsPlaying(sound.name))
                {
                    AudioManager.Pause(sound.name);
                }
            }
            AudioManager.Play(SongName);
            gameObject.SetActive(false);
        }
    }
}
