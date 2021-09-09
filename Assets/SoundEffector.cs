using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundEffector : MonoBehaviour
{
    public enum TypeFloor
    {
        rock,
        leaf,
        snow,
        water,
        grass
    }

    private TypeFloor flour;
    [SerializeField] private AudioClip[] SoundRockStep;
    [SerializeField] private AudioClip[] SoundGrassStep;
    [SerializeField] private AudioClip[] SoundWaterStep;
    [SerializeField] private AudioClip[] SoundSnowStep;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        flour = TypeFloor.rock;
    }

    public void PlaySound()
    {
        AudioClip clip = null;
        switch (flour)
        {
            case TypeFloor.rock:
                clip = SoundRockStep[Random.Range(0, SoundRockStep.Length)];
                break;
            case TypeFloor.leaf:
                break;
            case TypeFloor.snow:
                clip = SoundSnowStep[Random.Range(0, SoundSnowStep.Length)];
                break;
            case TypeFloor.water:
                clip = SoundWaterStep[Random.Range(0, SoundWaterStep.Length)];
                break;
            case TypeFloor.grass:
                clip = SoundGrassStep[Random.Range(0, SoundGrassStep.Length)];
                break;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        var tempCollider = other.GetComponent<SoundTrigger>();
        if (tempCollider != null)
        {
            flour = tempCollider.TypeFloor;
        }
    }
}