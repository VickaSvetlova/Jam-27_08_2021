using System;
using System.Collections;
using UnityEngine;

public class Idea : MonoBehaviour
{
    public event Action<Idea> OnDestroyIdea;

    public enum ColorIdea
    {
        red,
        green,
        blue
    }

    [SerializeField] private ColorIdea colorIdea;
    [SerializeField] private Renderer renderer;
    [SerializeField] private float timeLifeIdea;
    [SerializeField] private float minDebaf = 2;
    [SerializeField] private float maxDebaf = 10;
    private float coliderR;
    private float currentTimeIdea;
    private IEnumerator timer;
    private SphereCollider _collider;

    private float debaf = 1;

    public void SetDebaf(float debaf)
    {
        if (debaf == 1)
        {
            this.debaf = 1;
            return;
        }

        this.debaf = (maxDebaf - minDebaf) * debaf + minDebaf;
        //  Debug.Log("debaf " + this.debaf);
    }

    public ColorIdea Color_Idea
    {
        get => colorIdea;
        set => colorIdea = value;
    }

    public float CurrentTimeIdea
    {
        get => currentTimeIdea;
        set => currentTimeIdea = value;
    }

    public float TimeLifeIdea
    {
        get => timeLifeIdea;
        set => timeLifeIdea = value;
    }

    private void Start()
    {
        SetColor(Color_Idea);
        _collider = GetComponent<SphereCollider>();
    }

    public void TakeIdea()
    {
        StartCoroutine(Timer());
        _collider.enabled = false;
    }

    private void SetColor(ColorIdea color)
    {
        switch (color)
        {
            case ColorIdea.red:
                renderer.materials[0].color = Color.red;
                break;
            case ColorIdea.green:
                renderer.materials[0].color = Color.green;
                break;
            case ColorIdea.blue:
                renderer.materials[0].color = Color.blue;
                break;
        }
    }

    IEnumerator Timer()
    {
        currentTimeIdea = TimeLifeIdea;
        while (currentTimeIdea > 0)
        {
            yield return null;
            currentTimeIdea -= Time.deltaTime * debaf;
            renderer.materials[0].SetFloat("_FillAmount", 1f - currentTimeIdea / TimeLifeIdea);
//            Debug.Log("saturation " + saturation);
        }

        OnDestroyIdea?.Invoke(this);
    }
    
}