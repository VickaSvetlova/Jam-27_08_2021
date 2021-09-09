using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Creater : MonoBehaviour
{
    public event Action<Idea.ColorIdea, float> OnColor;
    [SerializeField] private Image UIColorFill;
    [SerializeField] private Idea.ColorIdea needColorIdea;
    [SerializeField] private float maxMeter;
    [SerializeField] private Renderer renderer;
    private float CurrentMeter = 0;
    private IEnumerator timer;
    [SerializeField] private float speed = .3f;

    private void Start()
    {
        switch (needColorIdea)
        {
            case Idea.ColorIdea.red:
                UIColorFill.GetComponent<Image>().color = Color.red;
                renderer.materials[0].color = Color.red;

                break;
            case Idea.ColorIdea.green:
                UIColorFill.GetComponent<Image>().color = Color.green;
                renderer.materials[0].color = Color.green;
                break;
            case Idea.ColorIdea.blue:
                UIColorFill.GetComponent<Image>().color = Color.blue;
                renderer.materials[0].color = Color.blue;
                break;
        }

        if (CurrentMeter > 0)
        {
            UIColorFill.fillAmount = CurrentMeter / maxMeter;
            OnColor?.Invoke(needColorIdea, CurrentMeter);
        }
        else
        {
            UIColorFill.fillAmount = 0;
            OnColor?.Invoke(needColorIdea, CurrentMeter);
        }
    }

    public void TakeColor(Idea.ColorIdea colorIdea, float colorFill)
    {
        if (colorIdea == needColorIdea)
        {
            CurrentMeter += colorFill;
            if (CurrentMeter > maxMeter)
            {
                CurrentMeter = maxMeter;
                OnColor?.Invoke(needColorIdea, CurrentMeter);
            }

            if (timer == null)
            {
                timer = Timer();
                StartCoroutine(timer);
            }
        }
        else
        {
            Debug.Log("идея потеряна!");
        }
    }

    IEnumerator Timer()
    {
        while (CurrentMeter > 0)
        {
            CurrentMeter -= speed * Time.deltaTime;
            yield return null;
            UIColorFill.fillAmount = CurrentMeter / maxMeter;
            OnColor?.Invoke(needColorIdea, CurrentMeter);
        }

        timer = null;
    }
}