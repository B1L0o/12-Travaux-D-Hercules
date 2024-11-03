using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneToPlaySoundCongratulationTuto : MonoBehaviour
{
    public AudioManager AudioManager;
    public string SongName;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        var listOfZoneVoice = GameObject.FindGameObjectsWithTag("VoiceZone");
        if (col.gameObject.CompareTag("Player") && listOfZoneVoice.Length==2)
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
