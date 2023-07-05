using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class soundManager : MonoBehaviour
{

    public static soundManager Instance;

    public sound[] sounds;

    private float timeToPlay = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        foreach(sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volumen;
            s.audioSource.pitch = s.pitch;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.loop = s.loop;
            s.audioSource.mute = s.mute;
        }
    }

    public void Play(string name)
    {
        if(name == "Motor")
        {
            if(canPlay(name))
            {
                sound s = Array.Find(sounds, s => s.name == name);
                if(s != null) 
                {
                    s.audioSource.PlayOneShot(s.clip);
                }
            }
            
        }
        else
        {
            sound s = Array.Find(sounds, s => s.name == name);

            if(s != null) s.audioSource.PlayOneShot(s.clip);
        }
        
    }

    public void Stop(string name)
    {
        sound s = Array.Find(sounds, s => s.name == name);

        if(s != null) s.audioSource.Stop();
    }

    private bool canPlay(string name)
    {
        bool answer = false;

        sound s = Array.Find(sounds, s => s.name == name);

        if(s.name == "Motor")
        {
            float nexTimeToPlay = 2f;
            if(timeToPlay + nexTimeToPlay < Time.time)
            {   
                timeToPlay = Time.time;
                answer = true;
            }
        }

        return answer;
    }
}
