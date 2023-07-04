using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class sound // Porque no es un GameObject como tal, simplemente es una clase que guarda valores
{

    //Dispongo todas las características de un sonido
    public string name;
    public AudioClip clip;
    [Range(0, 1)]
    public float volumen;
    [Range(-3, 3)]
    public float pitch; 
    public bool playOnAwake;
    public bool loop;
    public bool mute;

    public AudioSource audioSource;
}