using System;
using System.Collections;
using UnityEngine;

public class SpawnIdeasSystem : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private GameObject ideaPref;
    [SerializeField] private Transform[] pointsSpawn;
    [SerializeField] private float timerRespawn;
    [SerializeField] private Idea.ColorIdea colorIdeaOnArea;
    [SerializeField] private float ideaCount;

    public float IdeaCount
    {
        get => ideaCount;
        set => ideaCount = value;
    }

    private void Start()
    {
        SpawnIdeas();
        StartCoroutine(TimerRespawn());
    }

    private void SpawnIdeas()
    {
        foreach (var point in pointsSpawn)
        {
            if (!point.GetComponent<PointSpawn>().isBusy)
            {
                var clonIdea = Instantiate(ideaPref, point.transform.position, Quaternion.identity);
                var component = clonIdea.GetComponent<Idea>();
                component.Color_Idea = colorIdeaOnArea;
                component.timeLifeIdea = IdeaCount;
                clonIdea.transform.SetParent(origin);
            }
        }
    }

    IEnumerator TimerRespawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerRespawn);
            SpawnIdeas();
        }
    }
}