using System;
using System.Collections;
using _Project.Scripts.scriptables;
using _Project.Scripts.utils;
using UnityEngine;

namespace _Project.Scripts.radio_station
{
    public class RadioStation: Singleton<RadioStation>
    {
        [SerializeField] private RadioTracks radioTracks;
        private AudioSource audioSource1;
        [SerializeField] private AudioClip radioNoiseSound;

        private int currentStation = -1;
        
        public bool NextStation()
        {
            currentStation++;
            if (currentStation >= radioTracks.audioClips.Length)
            {
                currentStation = -1;
            }

            return PlayCurrentStation();
        }

        private bool PlayCurrentStation()
        {
            if (currentStation == -1 )
            {
                StopAllCoroutines();
                if (audioSource1.isPlaying)
                {
                    audioSource1.Stop();
                }
                return false;
            }

            StartCoroutine(PlayRadio());
            
            return true;
        }
        
        private IEnumerator PlayRadio()
        {
            audioSource1.clip = radioNoiseSound;
            audioSource1.loop = false;
            audioSource1.Play();
            yield return new WaitForSeconds(radioNoiseSound.length);
            audioSource1.clip = radioTracks.audioClips[currentStation];;
            audioSource1.loop = true;
            Debug.Log("realtimeSinceStartup: " + Time.realtimeSinceStartup);
            audioSource1.time = Time.realtimeSinceStartup - audioSource1.clip.length * Mathf.Floor(Time.realtimeSinceStartup / audioSource1.clip.length);
            Debug.Log("audioSource1.time: " + audioSource1.time);
            audioSource1.Play();
        }

        public void SetAudioSource(AudioSource audioSource1)
        {
            this.audioSource1 = audioSource1;
        }
    }
}