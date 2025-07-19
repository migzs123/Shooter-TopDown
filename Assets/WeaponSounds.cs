using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSounds : MonoBehaviour
{
    public AudioClip soundEffect; // Arraste seu som para este campo no Inspector

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // M�todo para tocar o som uma vez
    public void PlaySound()
    {
        audioSource.PlayOneShot(soundEffect);
    }

    // M�todo para tocar o som configurado no AudioSource
    public void Play()
    {
        audioSource.Play();
    }

    // M�todo para parar o som
    public void Stop()
    {
        audioSource.Stop();
    }
}
