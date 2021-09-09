using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SoundAmbientSystem : MonoBehaviour
{
    public enum AreaSound
    {
        rock,
        city,
        fores,
        office
    }

    private AreaSound areaSound;
    [SerializeField] private AudioClip rock;
    [SerializeField] private AudioClip city;
    [SerializeField] private AudioClip forest;
    [SerializeField] private AudioClip office;

    [SerializeField] private AudioSource current;
    [SerializeField] private AudioSource last;
    [SerializeField] private float speedTransition = 1;
    private AudioClip currentClip;
    private AudioClip lastClip;

    private void Start()
    {
        areaSound = AreaSound.city;
        PlaySound(areaSound);
    }

    private IEnumerator playSound;

    public void PlaySound(AreaSound areaSound)
    {
        // if (playSound != null) StopCoroutine(playSound);
        // playSound = SoundTransition();
        // this.areaSound = areaSound;
        switch (areaSound)
        {
            case AreaSound.rock:
                currentClip = rock;
                break;
            case AreaSound.city:
                currentClip = city;
                break;
            case AreaSound.fores:
                currentClip = forest;
                break;
            case AreaSound.office:
                currentClip = office;
                break;
        }

        current.clip = currentClip;
        current.Play();
        this.areaSound = areaSound;
        // current.clip = currentClip;
        // last.clip = lastClip;
        // current.volume = 0;
        // last.volume = 100;
        // current.Play();
        // last.Play();
        // StartCoroutine(playSound);
        // lastClip = currentClip;
    }

    IEnumerator SoundTransition()
    {
        while (last.volume > 0)
        {
            current.volume -= Time.deltaTime * speedTransition;
            last.volume += Time.deltaTime * speedTransition;
            yield return null;
        }

        playSound = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        return;
        var tempCollider = other.GetComponent<MuzicTrigger>();
        if (tempCollider != null)
        {
            if (tempCollider.AreaSound != areaSound)
            {
                PlaySound(tempCollider.AreaSound);
            }
        }
    }
}