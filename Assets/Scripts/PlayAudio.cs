using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayAudioSorces()
    {
        _audioSource.Play();
    }
}